using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ticket.Shared.DTOs;

public class ResponseDto<T>
{
    public T Data { get; private set; }

    [JsonIgnore]
    public int StatusCode { get; private set; }

    [JsonIgnore]
    public bool isSuccessful { get; private set; }

    public List<string> Errors { get; set; }

    public static ResponseDto<T> Success(T data, int statusCode)
    {
          return new ResponseDto<T> { StatusCode = statusCode, Data = data, isSuccessful=true};
    }
    public static ResponseDto<T> Success(int statusCode)
    {
        return new ResponseDto<T> { Data = default(T), StatusCode= statusCode, isSuccessful = true };
    }
    public static ResponseDto<T> Fail(int statusCode, List<string> errors)
    {
        return new ResponseDto<T> { Errors = errors, StatusCode = statusCode, isSuccessful=false};
    }
    //public static ResponseDto<T> Fail(int statusCode, string error)
    //{
    //    return new ResponseDto<T> { Errors = new List<string> { error }, StatusCode = statusCode, isSuccessful = false };
    //}

}
