using System;

namespace CLIQ_UE.Helpers
{
    public class FormatEmail
    {
        public static string CreateDesignForForgotPassword(string url, string email, string token)
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



		public static string CreateDesignForConfirmEmail(string code)
        {
			var em = $@"<!DOCTYPE html
                        PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
                        <html>

                        <head>
                          <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
                          <meta name=""color-scheme"" content=""light dark"">
                          <meta name=""supported-color-schemes"" content=""light dark"">
                          <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" maximum-scale=1"">
                          <link href=""https://fonts.googleapis.com/css?family=Raleway:300,400,700,900"" id=""google-fonts-css"" media=""all""
                            rel=""stylesheet"" type=""text/css"" />
                          <style>
                            body {{
                              width: 100vw;
                            }}

                            .innermain {{
                              width: 528px !important;
                            }}

                            #hello {{
                              background-image: url('https://res.cloudinary.com/gabbyprecious/image/upload/v1645116374/l0orrgcywgv35eqf4xcl.jpg') !important;
                              background-size: cover;
                            }}

                            @media only screen and (device-width: 375px) and (device-height: 812px) and (-webkit-device-pixel-ratio: 3) {{
                              .innermain {{
                                width: 400px !important;
                              }}
                            }}

                            @media only screen and (device-width: 390px) and (device-height: 844px) and (-webkit-device-pixel-ratio: 3) {{
                              .innermain {{
                                width: 400px !important;
                              }}
                            }}

                            @media only screen and (device-width: 428px) and (device-height: 926px) and (-webkit-device-pixel-ratio: 3) {{
                              .innermain {{
                                width: 430px !important;
                              }}
                            }}
                          </style>

                        </head>

                        <body leftmargin=""0"" marginwidth=""0"" topmargin=""0"" marginheight=""0"" offset=""0"" style=""margin: 0pt auto; padding: 0px;"">
                          <div id=""hello"">
                            <table id=""main"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                              <tbody>
                                <tr>
                                  <td valign=""top"">
                                    <table class=""innermain"" cellpadding=""0"" cellspacing=""0"" border=""0"" align=""center""
                                      style=""margin:0 auto; table-layout: fixed;"">
                                      <tbody>
                                        <tr>
                                          <td colspan=""3"">
                                            <table cellpadding=""0"" align=""center"" cellspacing=""0"" border=""0"" bgcolor=""#E9FFF8""
                                              style=""border-radius: 4px; box-shadow: 0 2px 8px rgba(0,0,0,0.05);"">
                                              <tbody>
                                                <tr>
                                                  <td>
                                                    <a href=""https://www.bitnob.com""
                                                      style=""display: block;cursor:pointer;text-align:center;margin-left: auto;margin-right: auto;"">
                                                      <img src=""https://i.postimg.cc/gksrQtXM/Clique-Logo.png"" alt=""Clique-Logo""
                                                        style=""margin: auto;"" width=""150"" />
                                                    </a>
                                                  </td>
                                                </tr>
                                                <tr>
                                                  <td valign=""top"" align=""center"">
                                                    <div style=""margin-top: 40px;"">
                                                      <p style=""
                                                      font-size: 25px;
                                                      color: #000!important;
                                                      font-weight: 300;
                                                      font-family: 'Raleway', Helevetica, sans-serif;
                                                  "">
                                                        Hello! 👋
                                                      </p>
                                                    </div>
                                                  </td>
                                                </tr>
                                                <tr>
                                                  <td height=""40""></td>
                                                </tr>
                                                <tr
                                                  style=""font-family: 'Raleway', Helvetica, Arial, sans-serif;; color:#4E5C6E; font-size:14px; line-height:20px; margin-top:20px;"">
                                                  <td class=""content"" colspan=""2"" valign=""top"" align=""center""
                                                    style=""padding-left:90px; padding-right:90px;"">
                                                    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" bgcolor=""#E9FFF8"">
                                                      <tbody>
                                                        <tr>
                                                          <td align=""center"" valign=""bottom"" colspan=""2"" cellpadding=""3"">
                                                            <img src=""https://i.postimg.cc/mrHHCS8Z/mobile-otp-1.png"" alt=""phone"" />
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <td height=""30"" &nbsp;=""""></td>
                                                        </tr>
                                                        <tr>
                                                          <td align=""center"">
                                                            <div
                                                              style=""font-size: 16px; line-height: 32px; font-weight: 300; margin-left: 20px; margin-right: 20px; margin-bottom: 25px;"">
                                                              Below is your Email
                                                              Verification Code.
                                                            </div>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <td height=""24"" &nbsp;=""""></td>
                                                        </tr>
                                                        <tr>
                                                          <td height=""1"" align=""center"">
                                                            <table border=""0"" cellspacing=""1"" cellpadding=""0"">
                                                              <tr>
                                                                <td style=""padding: 20px 0; text-align: center; font-size: 30px;
                                                                                   font-family: 'Raleway', Helvetica, Arial, sans-serif;"">
                                                                  <h1> {code}
                                                                  </h1>
                                                              </tr>
                                                            </table>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <td height=""24"" &nbsp;=""""></td>
                                                        </tr>
                                                      </tbody>
                                                    </table>
                                                  </td>
                                                </tr>
                                                <tr>
                                                  <td height=""60""></td>
                                                </tr>
                                              </tbody>
                                            </table>
                                          </td>
                                        </tr>
                                      </tbody>
                                    </table>
                                  </td>
                                </tr>
                              </tbody>
                            </table>
                          </div>
                        </body>

                        </html>";
			return em;
		}

	}
}
