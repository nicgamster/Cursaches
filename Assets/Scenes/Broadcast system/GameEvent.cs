public static class GameEvent //Этот сценарий задает константу для пары сообщений о событиях, что позволяет
							  //систематизировать сообщения, одновременно избавляя от необходимости вводить строку
							  //сообщения в разных местах
{
	public const string BOX_CAME = "BOX_CAME";
	public const string BOX_OUT = "BOX_OUT";
	public const string END_MIS = "END_MIS";
	public const string MONEYCHANGED = "MONEYCHANGED";
	public const string TIMETOMOVE = "TIMETOMOVE";
}