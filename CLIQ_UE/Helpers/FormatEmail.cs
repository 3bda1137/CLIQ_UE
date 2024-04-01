namespace CLIQ_UE.Helpers
{
    public class FormatEmail
    {
        public static string CreateDesignOfEmail(string url, string email, string token)
        {
            var em = $@"
                <!DOCTYPE html>
                <html lang=""en"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Password Reset</title>
                    <style>
                        body {{
                            background-color: #f8f9fa;
                            font-family: Arial, sans-serif;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 50px auto;
                            padding: 20px;
                        }}
                        .card {{
                            border: none;
                            border-radius: 15px;
                            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
                            position: relative;
                        }}
                        .card-header {{
                            background-color: #007bff;
                            color: #fff;
                            border-radius: 15px 15px 0 0;
                            text-align: center;
                            padding: 20px;
                            font-weight: bold;
                            font-size: 24px;
                        }}
                        .card-body {{
                            padding: 30px;
                        }}
                        .btn-primary {{
                            background-color: #007bff;
                            border: none;
                            border-radius: 5px;
                            padding: 12px 24px;
                            color: #fff;
                            text-decoration: none;
                            display: inline-block;
                            font-size: 18px;
                            transition: background-color 0.3s;
                        }}
                        .btn-primary:hover {{
                            background-color: #0056b3;
                            color: #fff;
                        }}
                        .card-text {{
                            font-size: 18px;
                            line-height: 1.6;
                            margin-bottom: 20px;
                            text-align: center;
                        }}
                        .text-muted {{
                            color: #777;
                        }}
                        .text-center {{
                            text-align: center;
                        }}
                        .text-primary {{
                            color: #007bff;
                        }}
                        .text-info {{
                            color: #17a2b8;
                        }}
                        .text-danger {{
                            color: #dc3545;
                        }}
                    </style>
                </head>
                <body>
                    <div class=""container"">
                        <div class=""card"">
                            <div class=""card-header"">
                                Password Reset
                            </div>
                            <div class=""card-body"">
                                <form action=""{url}"" method=""post"">
                                    <input type=""hidden"" name=""email"" value=""{email}"">
                                    <input type=""hidden"" name=""token"" value=""{token}"">
                                    <p class=""card-text"">Hello,</p>
                                    <p class=""card-text"">We received a request to reset your password. To reset your password, click the button below:</p>
                                    <input type=""submit"" class=""btn btn-primary btn-block"" value=""Reset Password"">
                                </form>
                                <p class=""card-text"">If you did not request a password reset, please ignore this email.</p>
                                <p class=""card-text text-muted"">Thanks,<br>CLIQ UE</p>
                            </div>
                        </div>
                    </div>
                </body>
                </html>";
            return em;
        }

    }
}
