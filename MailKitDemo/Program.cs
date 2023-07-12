using MimeKit;

var message = new MimeMessage();
var fromEmail = new MailboxAddress("劉長庚", "dannyliu@test.com");
var toEmail = new MailboxAddress("測試信箱", "dannytest@test.com");
message.From.Add(fromEmail);
message.To.Add(toEmail);
message.Subject = "主旨：測試 MimeKit";

var builder = new BodyBuilder
{
    HtmlBody = @"<p>使用 MimeKit 進行 SMTP 寄信</p>"
};
message.Body = builder.ToMessageBody();

using (var client = new MailKit.Net.Smtp.SmtpClient())
{
    try
    {
        client.Connect("123.234.222.113", 465, true);
        // 設定傳送人的信箱帳號密碼
        // IIS SMTP 預設不用帳號密碼，跟用舊的 SmtpClient 不同， 此時在 Mailkit 中仍傳驗證的話會報錯，此時可直接註解這段...
        client.Authenticate(new System.Net.NetworkCredential("dannyliu@test.com", "yourPassword"));

        client.Send(message);

        Console.WriteLine($"成功：寄給 {toEmail.Name} 主旨：{message.Subject}");
    }
    catch (Exception ex)
    {
        Console.WriteLine("失敗！");
        Console.WriteLine(ex.ToString());
    }
}