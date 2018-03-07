using FluentValidation;
using System;
using System.Linq;
using FluentValidation.Results;

namespace Sino.Runtime.Validation
{
	/// <summary>
	/// 验证
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class BaseRequestValidator<T> : AbstractValidator<T>
	{
		/// <summary>
		/// 参数验证失败代码
		/// </summary>
		private static int ErrorCode { get; set; } = -3;

		/// <summary>
		/// 重写参数验证基类（不通过抛异常）
		/// </summary>
		public override ValidationResult Validate(ValidationContext<T> context)
		{
			var result = base.Validate(context);

			if (!result.IsValid && result.Errors != null && result.Errors.Count > 0)
			{
				var error = result.Errors.FirstOrDefault();
				int code;
				if (!int.TryParse(error.ErrorCode, out code))
				{
					code = ErrorCode;
				}
				throw new SinoException(error.ErrorMessage, code);
			}

			return result;
		}

		/// <summary>
		/// 验证Guid
		/// </summary>
		public bool ParameterIsGuid(string Parameter)
		{
			Guid result;
			return Guid.TryParse(Parameter, out result);
		}

		/// <summary>
		/// 验证枚举
		/// </summary>
		public bool ParameterIsEnum<E>(string Parameter)
		{
			int result;
			if (int.TryParse(Parameter, out result))
				return Enum.IsDefined(typeof(E), result);
			return false;
		}

		/// <summary>
		/// 验证日期
		/// </summary>
		public bool ParameterIsDateTime(string Parameter)
		{
			DateTime result;
			return DateTime.TryParse(Parameter, out result);
		}

		/// <summary>
		/// 验证Long
		/// </summary>
		public bool ParameterIsLong(string Parameter)
		{
			long result;
			return long.TryParse(Parameter, out result);
		}
	}
}