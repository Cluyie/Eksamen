using System;

namespace Business_Layer.Models
{
	public enum ApiResponseCode : int
	{
		NotAuthenticated = 0,
		Success = 1
	}

	public class ApiResponse<T>
	{
		public int Code
		{
			public get; private set;
		}

		public T Value
		{
			public get; private set;
		}

		public ApiResponse(int code, T value)
		{
			Code = code;
			Value = value;
		}
	}
}