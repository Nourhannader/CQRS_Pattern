namespace CQRS.Features.Shared
{
    public record RequestResponse<T>(
         T? Data,
         string Message = "",
         string MessageAr = "",
         bool IsSuccess = true
     )
    {
        public static RequestResponse<T> Success(
            T data,
            string message= "Operation completed successfully",
            string messageAr = "تمت العملية بنجاح"
           ) => new (data, message, messageAr, true);

        public static RequestResponse<T> Failure(
            string message = "Operation failed",
            string messageAr = "فشلت العملية"
            ) => new (default!, message, messageAr, false);
    }
}
