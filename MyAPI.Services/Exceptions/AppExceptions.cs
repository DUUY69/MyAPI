namespace MyAPI.Services.Exceptions
{
    public static class AppExceptions
    {
        public static AppException NotFoundId()
        {
            return new AppException("Entity not found with the given ID");
        }

        public static AppException NotFoundAccount()
        {
            return new AppException("Account not found or invalid credentials");
        }

        public static AppException BadRequest(string message)
        {
            return new AppException($"Bad request: {message}");
        }

        public static AppException Conflict(string message)
        {
            return new AppException($"Conflict: {message}");
        }
    }
}
