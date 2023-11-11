namespace WebApi.Helpers;

public class Logger 
{
    public static void LogAction(String action)
    {
      PrintLog("admin executed action: " + action);
    }

    public static void LogAction(int userId, String action)
    {
      PrintLog("user with id: " + userId + " executed action: " + action);
    }

    private static void PrintLog(String message){
      Console.WriteLine("[LOG: " + DateTime.Now + "] " + message);
    }
}
