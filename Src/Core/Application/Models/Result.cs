namespace Application.Models
{
  public class Result
  {
    public bool Success { get; private set; }
    public object? Data { get; private set; }

    public Result(bool success, object? data)
    {
      Success = success;
      Data = data;
    }
  }
}