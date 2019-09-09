using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Net;//新加的

/// <summary>
/// SymmetricMethod 的摘要说明
/// </summary>
public class SymmetricMethod
{

    public string Key;
    private SymmetricAlgorithm mobjCryptoService;
    public static String GetIP()
    {
        IPHostEntry hostip = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress[] addrList = hostip.AddressList;
        return addrList[0].ToString();
    }
    public SymmetricMethod()
    {
        this.mobjCryptoService = new RijndaelManaged();
        this.Key = Get_key1() + GetIP();
    }
    internal string Get_key1()
    {
        byte[] Get_key1 = new byte[344];
        Get_key1[0] = 103;
        Get_key1[1] = 0;
        Get_key1[2] = 74;
        Get_key1[3] = 0;
        Get_key1[4] = 71;
        Get_key1[5] = 0;
        Get_key1[6] = 70;
        Get_key1[7] = 0;
        Get_key1[8] = 69;
        Get_key1[9] = 0;
        Get_key1[10] = 84;
        Get_key1[11] = 0;
        Get_key1[12] = 78;
        Get_key1[13] = 0;
        Get_key1[14] = 121;
        Get_key1[15] = 0;
        Get_key1[16] = 119;
        Get_key1[17] = 0;
        Get_key1[18] = 82;
        Get_key1[19] = 0;
        Get_key1[20] = 121;
        Get_key1[21] = 0;
        Get_key1[22] = 82;
        Get_key1[23] = 0;
        Get_key1[24] = 74;
        Get_key1[25] = 0;
        Get_key1[26] = 99;
        Get_key1[27] = 0;
        Get_key1[28] = 111;
        Get_key1[29] = 0;
        Get_key1[30] = 89;
        Get_key1[31] = 0;
        Get_key1[32] = 107;
        Get_key1[33] = 0;
        Get_key1[34] = 83;
        Get_key1[35] = 0;
        Get_key1[36] = 50;
        Get_key1[37] = 0;
        Get_key1[38] = 73;
        Get_key1[39] = 0;
        Get_key1[40] = 90;
        Get_key1[41] = 0;
        Get_key1[42] = 117;
        Get_key1[43] = 0;
        Get_key1[44] = 68;
        Get_key1[45] = 0;
        Get_key1[46] = 99;
        Get_key1[47] = 0;
        Get_key1[48] = 75;
        Get_key1[49] = 0;
        Get_key1[50] = 78;
        Get_key1[51] = 0;
        Get_key1[52] = 103;
        Get_key1[53] = 0;
        Get_key1[54] = 69;
        Get_key1[55] = 0;
        Get_key1[56] = 65;
        Get_key1[57] = 0;
        Get_key1[58] = 113;
        Get_key1[59] = 0;
        Get_key1[60] = 43;
        Get_key1[61] = 0;
        Get_key1[62] = 66;
        Get_key1[63] = 0;
        Get_key1[64] = 66;
        Get_key1[65] = 0;
        Get_key1[66] = 113;
        Get_key1[67] = 0;
        Get_key1[68] = 74;
        Get_key1[69] = 0;
        Get_key1[70] = 53;
        Get_key1[71] = 0;
        Get_key1[72] = 57;
        Get_key1[73] = 0;
        Get_key1[74] = 78;
        Get_key1[75] = 0;
        Get_key1[76] = 103;
        Get_key1[77] = 0;
        Get_key1[78] = 122;
        Get_key1[79] = 0;
        Get_key1[80] = 82;
        Get_key1[81] = 0;
        Get_key1[82] = 76;
        Get_key1[83] = 0;
        Get_key1[84] = 112;
        Get_key1[85] = 0;
        Get_key1[86] = 54;
        Get_key1[87] = 0;
        Get_key1[88] = 55;
        Get_key1[89] = 0;
        Get_key1[90] = 43;
        Get_key1[91] = 0;
        Get_key1[92] = 109;
        Get_key1[93] = 0;
        Get_key1[94] = 121;
        Get_key1[95] = 0;
        Get_key1[96] = 111;
        Get_key1[97] = 0;
        Get_key1[98] = 74;
        Get_key1[99] = 0;
        Get_key1[100] = 57;
        Get_key1[101] = 0;
        Get_key1[102] = 114;
        Get_key1[103] = 0;
        Get_key1[104] = 88;
        Get_key1[105] = 0;
        Get_key1[106] = 81;
        Get_key1[107] = 0;
        Get_key1[108] = 112;
        Get_key1[109] = 0;
        Get_key1[110] = 69;
        Get_key1[111] = 0;
        Get_key1[112] = 99;
        Get_key1[113] = 0;
        Get_key1[114] = 116;
        Get_key1[115] = 0;
        Get_key1[116] = 66;
        Get_key1[117] = 0;
        Get_key1[118] = 51;
        Get_key1[119] = 0;
        Get_key1[120] = 72;
        Get_key1[121] = 0;
        Get_key1[122] = 107;
        Get_key1[123] = 0;
        Get_key1[124] = 52;
        Get_key1[125] = 0;
        Get_key1[126] = 51;
        Get_key1[127] = 0;
        Get_key1[128] = 57;
        Get_key1[129] = 0;
        Get_key1[130] = 70;
        Get_key1[131] = 0;
        Get_key1[132] = 111;
        Get_key1[133] = 0;
        Get_key1[134] = 99;
        Get_key1[135] = 0;
        Get_key1[136] = 65;
        Get_key1[137] = 0;
        Get_key1[138] = 100;
        Get_key1[139] = 0;
        Get_key1[140] = 68;
        Get_key1[141] = 0;
        Get_key1[142] = 71;
        Get_key1[143] = 0;
        Get_key1[144] = 77;
        Get_key1[145] = 0;
        Get_key1[146] = 56;
        Get_key1[147] = 0;
        Get_key1[148] = 57;
        Get_key1[149] = 0;
        Get_key1[150] = 87;
        Get_key1[151] = 0;
        Get_key1[152] = 65;
        Get_key1[153] = 0;
        Get_key1[154] = 117;
        Get_key1[155] = 0;
        Get_key1[156] = 122;
        Get_key1[157] = 0;
        Get_key1[158] = 116;
        Get_key1[159] = 0;
        Get_key1[160] = 103;
        Get_key1[161] = 0;
        Get_key1[162] = 84;
        Get_key1[163] = 0;
        Get_key1[164] = 49;
        Get_key1[165] = 0;
        Get_key1[166] = 54;
        Get_key1[167] = 0;
        Get_key1[168] = 75;
        Get_key1[169] = 0;
        Get_key1[170] = 97;
        Get_key1[171] = 0;
        Get_key1[172] = 89;
        Get_key1[173] = 0;
        Get_key1[174] = 73;
        Get_key1[175] = 0;
        Get_key1[176] = 49;
        Get_key1[177] = 0;
        Get_key1[178] = 65;
        Get_key1[179] = 0;
        Get_key1[180] = 112;
        Get_key1[181] = 0;
        Get_key1[182] = 105;
        Get_key1[183] = 0;
        Get_key1[184] = 73;
        Get_key1[185] = 0;
        Get_key1[186] = 55;
        Get_key1[187] = 0;
        Get_key1[188] = 79;
        Get_key1[189] = 0;
        Get_key1[190] = 113;
        Get_key1[191] = 0;
        Get_key1[192] = 103;
        Get_key1[193] = 0;
        Get_key1[194] = 98;
        Get_key1[195] = 0;
        Get_key1[196] = 105;
        Get_key1[197] = 0;
        Get_key1[198] = 101;
        Get_key1[199] = 0;
        Get_key1[200] = 54;
        Get_key1[201] = 0;
        Get_key1[202] = 66;
        Get_key1[203] = 0;
        Get_key1[204] = 87;
        Get_key1[205] = 0;
        Get_key1[206] = 122;
        Get_key1[207] = 0;
        Get_key1[208] = 103;
        Get_key1[209] = 0;
        Get_key1[210] = 112;
        Get_key1[211] = 0;
        Get_key1[212] = 82;
        Get_key1[213] = 0;
        Get_key1[214] = 102;
        Get_key1[215] = 0;
        Get_key1[216] = 90;
        Get_key1[217] = 0;
        Get_key1[218] = 99;
        Get_key1[219] = 0;
        Get_key1[220] = 114;
        Get_key1[221] = 0;
        Get_key1[222] = 104;
        Get_key1[223] = 0;
        Get_key1[224] = 108;
        Get_key1[225] = 0;
        Get_key1[226] = 117;
        Get_key1[227] = 0;
        Get_key1[228] = 49;
        Get_key1[229] = 0;
        Get_key1[230] = 85;
        Get_key1[231] = 0;
        Get_key1[232] = 67;
        Get_key1[233] = 0;
        Get_key1[234] = 106;
        Get_key1[235] = 0;
        Get_key1[236] = 114;
        Get_key1[237] = 0;
        Get_key1[238] = 57;
        Get_key1[239] = 0;
        Get_key1[240] = 51;
        Get_key1[241] = 0;
        Get_key1[242] = 110;
        Get_key1[243] = 0;
        Get_key1[244] = 86;
        Get_key1[245] = 0;
        Get_key1[246] = 82;
        Get_key1[247] = 0;
        Get_key1[248] = 43;
        Get_key1[249] = 0;
        Get_key1[250] = 70;
        Get_key1[251] = 0;
        Get_key1[252] = 105;
        Get_key1[253] = 0;
        Get_key1[254] = 110;
        Get_key1[255] = 0;
        Get_key1[256] = 82;
        Get_key1[257] = 0;
        Get_key1[258] = 76;
        Get_key1[259] = 0;
        Get_key1[260] = 79;
        Get_key1[261] = 0;
        Get_key1[262] = 102;
        Get_key1[263] = 0;
        Get_key1[264] = 81;
        Get_key1[265] = 0;
        Get_key1[266] = 43;
        Get_key1[267] = 0;
        Get_key1[268] = 120;
        Get_key1[269] = 0;
        Get_key1[270] = 74;
        Get_key1[271] = 0;
        Get_key1[272] = 121;
        Get_key1[273] = 0;
        Get_key1[274] = 73;
        Get_key1[275] = 0;
        Get_key1[276] = 81;
        Get_key1[277] = 0;
        Get_key1[278] = 97;
        Get_key1[279] = 0;
        Get_key1[280] = 113;
        Get_key1[281] = 0;
        Get_key1[282] = 66;
        Get_key1[283] = 0;
        Get_key1[284] = 88;
        Get_key1[285] = 0;
        Get_key1[286] = 54;
        Get_key1[287] = 0;
        Get_key1[288] = 56;
        Get_key1[289] = 0;
        Get_key1[290] = 73;
        Get_key1[291] = 0;
        Get_key1[292] = 80;
        Get_key1[293] = 0;
        Get_key1[294] = 72;
        Get_key1[295] = 0;
        Get_key1[296] = 77;
        Get_key1[297] = 0;
        Get_key1[298] = 71;
        Get_key1[299] = 0;
        Get_key1[300] = 113;
        Get_key1[301] = 0;
        Get_key1[302] = 89;
        Get_key1[303] = 0;
        Get_key1[304] = 56;
        Get_key1[305] = 0;
        Get_key1[306] = 65;
        Get_key1[307] = 0;
        Get_key1[308] = 81;
        Get_key1[309] = 0;
        Get_key1[310] = 68;
        Get_key1[311] = 0;
        Get_key1[312] = 106;
        Get_key1[313] = 0;
        Get_key1[314] = 67;
        Get_key1[315] = 0;
        Get_key1[316] = 66;
        Get_key1[317] = 0;
        Get_key1[318] = 71;
        Get_key1[319] = 0;
        Get_key1[320] = 90;
        Get_key1[321] = 0;
        Get_key1[322] = 111;
        Get_key1[323] = 0;
        Get_key1[324] = 99;
        Get_key1[325] = 0;
        Get_key1[326] = 105;
        Get_key1[327] = 0;
        Get_key1[328] = 69;
        Get_key1[329] = 0;
        Get_key1[330] = 101;
        Get_key1[331] = 0;
        Get_key1[332] = 109;
        Get_key1[333] = 0;
        Get_key1[334] = 111;
        Get_key1[335] = 0;
        Get_key1[336] = 69;
        Get_key1[337] = 0;
        Get_key1[338] = 71;
        Get_key1[339] = 0;
        Get_key1[340] = 48;
        Get_key1[341] = 0;
        Get_key1[342] = 61;
        Get_key1[343] = 0;
        string DNString_Get_key1 = System.Text.Encoding.Unicode.GetString(Get_key1);
        return DNString_Get_key1;
    }
    public string Decrypto(string Source)
    {
        try
        {
            byte[] buffer1 = Convert.FromBase64String(Source);
            MemoryStream stream1 = new MemoryStream(buffer1, 0, buffer1.Length);
            this.mobjCryptoService.Key = this.GetLegalKey();
            this.mobjCryptoService.IV = this.GetLegalIV();
            ICryptoTransform transform1 = this.mobjCryptoService.CreateDecryptor();
            CryptoStream stream2 = new CryptoStream(stream1, transform1, CryptoStreamMode.Read);
            StreamReader reader1 = new StreamReader(stream2);
            return reader1.ReadToEnd();
        }
        catch
        {
            return "";
        }
    }

    public string Encrypto(string Source)
    {
        byte[] buffer1 = Encoding.UTF8.GetBytes(Source);
        MemoryStream stream1 = new MemoryStream();
        this.mobjCryptoService.Key = this.GetLegalKey();
        this.mobjCryptoService.IV = this.GetLegalIV();
        ICryptoTransform transform1 = this.mobjCryptoService.CreateEncryptor();
        CryptoStream stream2 = new CryptoStream(stream1, transform1, CryptoStreamMode.Write);
        stream2.Write(buffer1, 0, buffer1.Length);
        stream2.FlushFinalBlock();
        stream1.Close();
        byte[] buffer2 = stream1.ToArray();
        return Convert.ToBase64String(buffer2);
    }

    internal string Get_key2()
    {
        byte[] key2 = new byte[128];
        key2[0] = 69;
        key2[1] = 0;
        key2[2] = 52;
        key2[3] = 0;
        key2[4] = 103;
        key2[5] = 0;
        key2[6] = 104;
        key2[7] = 0;
        key2[8] = 106;
        key2[9] = 0;
        key2[10] = 42;
        key2[11] = 0;
        key2[12] = 71;
        key2[13] = 0;
        key2[14] = 104;
        key2[15] = 0;
        key2[16] = 103;
        key2[17] = 0;
        key2[18] = 55;
        key2[19] = 0;
        key2[20] = 33;
        key2[21] = 0;
        key2[22] = 114;
        key2[23] = 0;
        key2[24] = 78;
        key2[25] = 0;
        key2[26] = 73;
        key2[27] = 0;
        key2[28] = 102;
        key2[29] = 0;
        key2[30] = 98;
        key2[31] = 0;
        key2[32] = 38;
        key2[33] = 0;
        key2[34] = 57;
        key2[35] = 0;
        key2[36] = 53;
        key2[37] = 0;
        key2[38] = 71;
        key2[39] = 0;
        key2[40] = 85;
        key2[41] = 0;
        key2[42] = 89;
        key2[43] = 0;
        key2[44] = 56;
        key2[45] = 0;
        key2[46] = 54;
        key2[47] = 0;
        key2[48] = 71;
        key2[49] = 0;
        key2[50] = 102;
        key2[51] = 0;
        key2[52] = 103;
        key2[53] = 0;
        key2[54] = 104;
        key2[55] = 0;
        key2[56] = 85;
        key2[57] = 0;
        key2[58] = 98;
        key2[59] = 0;
        key2[60] = 35;
        key2[61] = 0;
        key2[62] = 101;
        key2[63] = 0;
        key2[64] = 114;
        key2[65] = 0;
        key2[66] = 53;
        key2[67] = 0;
        key2[68] = 55;
        key2[69] = 0;
        key2[70] = 72;
        key2[71] = 0;
        key2[72] = 66;
        key2[73] = 0;
        key2[74] = 104;
        key2[75] = 0;
        key2[76] = 40;
        key2[77] = 0;
        key2[78] = 117;
        key2[79] = 0;
        key2[80] = 37;
        key2[81] = 0;
        key2[82] = 103;
        key2[83] = 0;
        key2[84] = 54;
        key2[85] = 0;
        key2[86] = 72;
        key2[87] = 0;
        key2[88] = 74;
        key2[89] = 0;
        key2[90] = 40;
        key2[91] = 0;
        key2[92] = 36;
        key2[93] = 0;
        key2[94] = 106;
        key2[95] = 0;
        key2[96] = 104;
        key2[97] = 0;
        key2[98] = 87;
        key2[99] = 0;
        key2[100] = 107;
        key2[101] = 0;
        key2[102] = 55;
        key2[103] = 0;
        key2[104] = 38;
        key2[105] = 0;
        key2[106] = 33;
        key2[107] = 0;
        key2[108] = 104;
        key2[109] = 0;
        key2[110] = 103;
        key2[111] = 0;
        key2[112] = 52;
        key2[113] = 0;
        key2[114] = 117;
        key2[115] = 0;
        key2[116] = 105;
        key2[117] = 0;
        key2[118] = 37;
        key2[119] = 0;
        key2[120] = 36;
        key2[121] = 0;
        key2[122] = 104;
        key2[123] = 0;
        key2[124] = 106;
        key2[125] = 0;
        key2[126] = 107;
        key2[127] = 0;
        string DNString_key2 = System.Text.Encoding.Unicode.GetString(key2);
        return DNString_key2;
    }

    private byte[] GetLegalIV()
    {
        string text1 = Get_key2();
        this.mobjCryptoService.GenerateIV();
        int num1 = this.mobjCryptoService.IV.Length;
        if (text1.Length > num1)
        {
            text1 = text1.Substring(text1.Length - num1, num1);
        }
        else if (text1.Length < num1)
        {
            text1 = text1.PadRight(num1, ' ');
        }
        return Encoding.ASCII.GetBytes(text1);
    }

    private byte[] GetLegalKey()
    {
        string text1 = this.Key;
        this.mobjCryptoService.GenerateKey();
        int num1 = this.mobjCryptoService.Key.Length;
        if (text1.Length > num1)
        {
            text1 = text1.Substring(text1.Length - num1, num1);
        }
        else if (text1.Length < num1)
        {
            text1 = text1.PadRight(num1, ' ');
        }
        return Encoding.ASCII.GetBytes(text1);
    }


}
