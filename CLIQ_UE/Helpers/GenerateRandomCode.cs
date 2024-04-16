namespace CLIQ_UE.Helpers
{
	public static class GenerateRandomCode
	{
		static string code;
		static GenerateRandomCode()
		{
			code = "";
		}

		public static string GetCode()
		{
			Random random = new Random();
			code = random.Next(10000000, 100000000).ToString();

			return code;
		}
	}
}
