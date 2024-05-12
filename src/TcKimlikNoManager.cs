using System;
using System.Globalization;
using System.Text.RegularExpressions;


namespace TCKimlikNo;

public class TcKimlikNoManager
{
    private readonly IClient _client;

    public TcKimlikNoManager(IClient client)
    {
        _client = client;
    }

    public async Task<bool> Validate(TcKimlikNoDto args)
    {
        var name = args.AutoUppercase ? args.Name.ToUpper(new CultureInfo("tr-TR")) : args.Name;
        var surname = args.AutoUppercase ? args.Surname.ToUpper(new CultureInfo("tr-TR")) : args.Surname;

        if (!Regex.IsMatch(name, "^[A-Z. ÇĞÖŞÜİ]+$", RegexOptions.Multiline))
        {
            return false;
        }


        if (!Regex.IsMatch(surname, "^[A-Z. ÇĞÖŞÜİ]+$", RegexOptions.Multiline))
        {
            return false;

        }

        if (!Regex.IsMatch(args.BirthYear.ToString(), @"^\d{4}$"))
        {
            return false;
        }


        if (!Verify(args.Tc))
        {
            return false;
        }
        
        var response = await _client.Post(new TcKimlikNoDto()
        {
            Name = name,
            Surname = surname,
            Tc = args.Tc,
            BirthYear = args.BirthYear
        });
        
        var responseContent = await response.Content.ReadAsStringAsync();
        
        return responseContent.Contains("<TCKimlikNoDogrulaResult>true</TCKimlikNoDogrulaResult>");
    }

    public bool Verify(string tcKimlikNo)
    {
        if (tcKimlikNo.Length != 11)
            return false;

        if (!Regex.IsMatch(tcKimlikNo, @"^[1-9]{1}[0-9]{9}[02468]{1}$"))
            return false;

        var checksumDigits = GenerateChecksumDigits(tcKimlikNo);

        if (checksumDigits[0] != tcKimlikNo[9])
            return false;

        if (checksumDigits[1] != tcKimlikNo[10])
            return false;

        return true;
    }

    private string GenerateChecksumDigits(string tcKimlikNo)
    {
        var oddDigitsSum = int.Parse(tcKimlikNo[0].ToString()) +
                           int.Parse(tcKimlikNo[2].ToString()) +
                           int.Parse(tcKimlikNo[4].ToString()) +
                           int.Parse(tcKimlikNo[6].ToString()) +
                           int.Parse(tcKimlikNo[8].ToString());

        var evenDigitsSum = int.Parse(tcKimlikNo[1].ToString()) +
                            int.Parse(tcKimlikNo[3].ToString()) +
                            int.Parse(tcKimlikNo[5].ToString()) +
                            int.Parse(tcKimlikNo[7].ToString());

        var digit10 = (oddDigitsSum * 7 - evenDigitsSum) % 10;
        var digit11 = (oddDigitsSum + evenDigitsSum + digit10) % 10;

        if (digit10 < 0)
            digit10 += 10;

        return digit10.ToString() + digit11.ToString();
    }
}