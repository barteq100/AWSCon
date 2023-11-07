using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Core;
using SharedLib;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSWeather;

public class Function
{

    /// <summary>
    /// A simple function that takes a string and returns both the upper and lower case version of the string.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Get, "/forecast")]
    public IEnumerable<WeatherForecast> FunctionHandler(ILambdaContext context)
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = WeatherForecast.Summaries[Random.Shared.Next(WeatherForecast.Summaries.Length)]
        }).ToArray();
    }
}
