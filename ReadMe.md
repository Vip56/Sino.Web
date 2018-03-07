# Sino.Web
该项目用于规范后期所有后台开发的规范并为后期更好的升级做好前期准备工作。   

[![Build status](https://ci.appveyor.com/api/projects/status/b64xdqtjcmj8syo9/branch/dev?svg=true)](https://ci.appveyor.com/project/vip56/sino-web/branch/dev)
[![NuGet](https://img.shields.io/nuget/v/Nuget.Core.svg?style=plastic)](https://www.nuget.org/packages/Sino.Web)   

## 如何使用
```
Install-Package Sino.Web
```

# 文档
下面将按照依次进行说明，我们将按照常规项目的流程来介绍如何使用该SDK中的特性。

## 领域层

#### 领域模型
按照规定所有的领域模型必须继承自`FullAuditedEntity<TPrimaryKey>`虚类，其中`TPrimaryKey`是用于规定主键的类型，为了能够和仓储
层自动配合建议使用`Guid`以及`Int`类型，为什么需要继承该模型，因为该模型中集成了我们正常开发中需要使用的常用字段，
具体如下所示：  
* `IsDeleted（bool）`：提供软删除功能，默认为False。
* `DeletionTime（DateTime?）`：删除的时间。
* `DeleterUserId（long?）`：执行删除操作的用户。
* `LastModificationTime（DateTime）`：最后编辑时间，所有的UPDATE操作。
* `LastModifierUserId（long?）`：最后编辑的用户，该该值赋值的同时`LastModificationTime`自动赋为当前时间。
* `CreationTime（DateTime）`：创建时间，创建模型的时候自动赋值为当前时间。
* `CreatorUserId（long?）`：创建的用户
* `Id`：根据`TPrimaryKey`类型所定

除了以上这些扩展的字段之外，还提供了其他的快捷方法：
* `IsNullOrDeleteed():bool`：如果当前实体为Null或IsDeleted为True则返回True，否则为False。
* `UnDelete`：撤销删除操作，即将`IsDeleted`设置为False，同时将`DeletionTime`和`DeleterUserId`设置为Null。

还需要注意其中部分自带的方法已经被重写从而符合真实的业务场景：
* `Equals`：判断依据为`Id`字段是否相等。
* `GetHashCode`：获取的哈希值为`Id`字段。
* `==`：判断依据为`Id`字段。
* `!=`：判断依据为`Id`字段。
* `ToString`：输出为`[GetType().Nmae] [Id]`。

如果实际的业务并不需要依赖如此多的审计功能，可以单独选择继承其他类：
* `AuditedEntity`：不包含删除审计功能。
* `CreationAuditedEntity`：只包含创建信息。
* `Entity`：不包含任何审计功能。

#### 领域服务
因为当前领域服务存在数量不多，所以只约定了需要统一继承的公共类为`DomainService`。

## 仓储层

#### 仓储接口
准确的说仓储层所定义的接口在项目结构上是保存在领域层的项目中，以此规范仓储层的实现。具体需要继承的仓储接口需要根据根据
对应的实体模型的情况而决定，如果实体模型的主键类型为`Int`则对应的仓储接口则需要继承自`IRepository<TEntity>`，如果不是
该类型则需要继承自`IRepository<TEntity, TPrimaryKey>`，他们都是默认有很多常用的方法，当然这些方案并不需要我们自行实现。  

可以发现我们接口中的`Task<Tuple<int, IList<TEntity>>> GetListAsync(IQueryObject<TEntity> query);`中有一个`IQueryObject`
这是为了能够以最小的成本来增加新的查询条件，这样我们不用在仓储中看到很多查询条件的语句了，真实编码中我们需要继承自`QueryObject<Entity>`
类，其中`QueryExpression`提供了具体查询语句的Lambda表达式，而为了防止SQL注入，我们来利用了`QuerySql`属性来进行参数的传递。
当然除了查询条件，列表中经常使用的还有字段的排序，这里我们也提供了`OrderField`字段用来记录，从而保证我们的列表查询是统一的，
就算一后期有变动也只需要修改对应的`QueryObject`对象即可。  
PS：这里我们也考虑过使用动态的方式，当时这种方式肯定会有所牺牲并且需要进行单独的后台管理，如果后期这部分的修改需求较多，我们
可能会考虑采用动态的方式进行管理。

#### 仓储实现
如果不使用我们其他的基础类库，则用户需要在仓储层实现类上除了需要实现具体的仓储接口外，还需要继承自`AbpRepositoryBase<TEntity, TPrimaryKey>`
虚类，以便于后期如果需要使用我们提供的类库降低切换的成本。

## 应用层（服务层）

#### 应用接口
在完成领域层的功能之后我们还不能直接就开始编写具体的接口，这中间还需要应用层进行协调，而所有的应用层接口都需要继承自
`IApplicationService`接口，这样便于后期在应用层中增加通用服务等，同时也便于后期我们的程序集扫描。

#### 应用实现
在我们实现具体的应用服务同时，除了需要实现具体业务的应用接口还需要继承公共的类库`ApplicationService`，以便提供公共的基础服务,
其中我们已经提供了部分服务，具体服务如下所示：
* `StringToGuid(s:String):Guid?`：将字符串转换为Guid类型，如果无法转换则返回Null。  

#### 数据传输对象（DTO）
在表现层与应用层的交互过程中还存在一种模型用于进行数据的传输，在单一应用的情况下该DTO可以暂时共享领域模型，但是对于分布式的应用
或微服务来说，该对象需要独立编写，而在该类库中也提供了一套标准的对象，所有的应用接口的入参必须为对象且需要实现接口`IInputDto`，
如果该应用层接口恰好是需要获取的列表数据并需要分页那么我们也提供了对应的模型`PagedInputDto`其中规范了`Skip`和`Take`参数，避免
出现不同方式的分页。  
讲述完输入剩下的就是输出，输出的DTO必须继承自`OutputDto`，对于输出是列表的还需要配合`EntityPageIndexDto<TPrimaryKey>`和`IPagedResult<T>`
进行组织数据。  
但是随着我们后期全面采用gRpc进行通信，这些标准会采用`base.proto`进行表示，所以具体gRcp的DTO模型请根据实际情况做调整。

## 表现层

#### 视图模型
对于大多情况来说，该组件已经采用`ASP.NET CORE`的技术进行的标准化，但是对于列表数据，用户需要利用`ListResponse<T>`进行组织，如果
遇到特殊情况需要自行输出最外层接口的数据可以参考模型`BaseResponse`和`BaseResponse<T>`。

#### 输出标准化
前面我说的大部分情况下输出会自动进行包装，但是要实现这一特性还需要其他的配置，我们需要打开StartUp文件并在`services.AddMvc`中进行注册
比如以下代码为注册:  
`
services.AddMvc(x =>
{
  x.Filters.Add(typeof(GlobalResultFilter));
});
`

但是其中有点要求，输出的只有为`EmptyResult`和`ObjectResult`才会进行包装就是最终的返回值为`void`以及`object`对象，当然`String`和`int`
等不属于`ObjectResult`，因为该行为会导致所有的输出都进行包装，为了让用户能够排除特定的接口，可以利用`[GlobalResult]`过滤不需要进行封装
的接口即可。


#### 接口校验
如今移动平台开始热门起来，但是基于当前的网络通信安全，仅仅利用Https和JWT方式并不能有效的防止用户信息外泄，为了防止请求内容被拦截后造成
严重的后果，我们还开发了接口校验的功能，会在每个接口请求前进行Token验证，并且该Token的有效时间可以控制，这样保证了请求被拦截后，超出有效
时间后该请求无法进行，进一步的提高了接口的安全系数。  
为了利用该特性，我们需要跟输出标准化的做法一样增加这一过滤器：  
`
CheckSignatureFilter.Token = "123456fefef";
services.AddMvc(x =>
{
     x.Filters.Add(typeof(CheckSignatureFilter));
});
`
其中的算法如下：  
`Signature=MD5(Token + timestamp + nonce)`  
* `Token`：需要预先设定好。
* `Timestamp`：Unix时间戳（单位秒）。
* `nonce`：随机字符串。

默认为120秒超时时间，如果有特定需要可以通过`CheckSignatureFilter.TimeOut`进行设置。对于不要进行校验的接口可以利用`[CheckSignature]`进行过滤。  

#### 参数验证
为了提高开发效率我们把参数的自动化验证功能利用FluentValidation解决了，想要在项目中利用该特性需打开`Startup`文件中进行注册。  
比如以下代码为注册：  
`
services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ModelValidator>());
`

示例中的`ModelValidator`是参数验证类命名空间下的其中之一（`任选一个`注册即可）。  
`Model`模型类建议加上注解属性`[Validator(typeof(ModelValidator))]`。  
`ModelValidator`参数验证类`必须继承BaseRequestValidator`。  

`ModelValidator`参数验证方法中还可指定ErrorCode和ErrorMessage动态返回给调用方。  
以下代码为示例：  
`
RuleFor(x => x.Name).NotEmpty().WithErrorCode("-8").WithMessage("名称不能为空");
`

## 异常处理

#### 异常标准
对于系统校验等出现的异常，统一采用规定的异常进行输出，对应的中间件会对其进行处理以标准化的格式进行输出，这样可以避免组织大量的模型并且还要逐层返回，
其中已经制订的异常如下：
* `SinoException`：公共异常，对于没有对应定义的异常可以采用该类。
* `SinoNotFoundException`：数据不存在等异常。
* `SinoOperationException`：某种操作出现问题时的异常。
* `SinoAlreadyExistException`：已存在相同数据时的异常。

为了兼容后期gRpc的异常，所以在利用gRpc后对应的异常进行调整否则无法自然的传递到表现层，具体对应如下：
* `SinoRpcException`：公共异常，对于没有对应定义的异常可以采用该类。
* `SinoNotFoundRpcException`：数据不存在等异常。
* `SinoAlreadyExistRpcException`：数据已存在异常

如果要启用全局异常捕获需要在`Startup`中进行配置：  
`
app.UseGlobalExceptionHandler(LogManager.GetCurrentClassLogger());
`


## 其他
#### JSON序列化
为了避免直接使用静态类，该类库中提供了`IJsonConvertProvider`接口将`JSON.NET`进行了封装，对于程序中需要使用到的地方请采用
该接口，该接口已经纳入IOC中，只需要在StartUp中进行添加即可：  
`
services.AddJson();
`

#### 自动IOC注入
为了避免在后期大量的服务需要手动注入，利用`Scrutor`类库实现了自动根据名字进行IOC注入，默认规则为根据类名前加`I`的方式自动进行
注入，当然还有继承了接口`ISingletonDependency`和`ITransientDependency`才可以被自动注入，并按照规定的方式进行，以下就是如何使用的方式：
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    services.AutoDependency(typeof(IAInterface),typeof(IBInterface));
```

#### 验证自动注入
为了方便验证，这里将原本原始的初始化方式改为自动IOC注入的方式，对于需要验证的对象需要如下所示来对该实体模型进行验证：
```
public class UserValidator : BaseRequestValidator<User>
{
    public UserValidator()
    {
         RuleFor(x => x.UserName).NotEmpty().WithErrorCode("1000").WithMessage("用户名不能为空")
            .Length(0, 20).WithErrorCode("1001").WithMessage("用户名不能小于0或大于20");
    }
}
```
对于需要使用该验证的则通过以下方式注入：
```
public ValuesController(IBaseRequestValidator<User> validator)
{
    validator.ValidateModel(new Models.User
    {
         UserName = "25458545edfrdfedswaqw"
    });
}
```
新的验证已经内部集成了自动抛出异常，默认会将第一个异常抛出，所以务必对于每种验证加上`WithErrorCode`和`WithMessage`否则会导致
验证异常。  

## 文档版本
* 2017.3.9 v1.0 起草 by y-z-f
* 2017.3.28 v1.1 增加自动IOC扫描 by y-z-f
* 2017.3.31 v1.1.22-beta2 将项目移到src下 by y-z-f
* 2017.3.31 v1.1.22-beta3 增加支持IOC的参数验证 by y-z-f
* 2018.3.7 v2.0.0-beta1 支持Asp.net Core 2.0并去除gRpc支持 by y-z-f

## 依赖类库
```
Dapper : 1.50.4   
FluentValidation : 7.5.0   
Microsoft.AspNetCore.Diagnostics : 2.0.0   
Microsoft.AspNetCore.Mvc.Core : 2.0.0    
Microsoft.Extensions.Caching.Abstractions : 2.0.0    
NLog : 4.5.0-rc06    
Scrutor : 2.2.1
```