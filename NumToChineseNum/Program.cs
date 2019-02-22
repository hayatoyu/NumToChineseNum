using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumToChineseNum
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(NumToChineseNum("45792"));
            Console.WriteLine(NumToChineseNum("100000"));
            Console.WriteLine(NumToChineseNum("205000"));
            Console.WriteLine(NumToChineseNum("5920001245"));
            Console.WriteLine(NumToChineseNum("10040002000"));
            Console.WriteLine(NumToChineseNum("200005000"));
            Console.WriteLine(NumToChineseNum("12000"));
            Console.WriteLine(NumToChineseNum("500000087"));
        }

        // 光用 long 沒辦法到 10^44 次方
        static string NumToChineseNum(string num)
        {
            string[] chineseNum = {"零","壹","貳","參","肆","伍","陸","柒","捌","玖" };
            string[] chineseUnit_low = { "","拾","佰","仟"};
            string[] chineseUnit_high = { "","萬","億","兆","京","垓","秭","穰","溝","澗","正","載"};            
            int inputLen = num.Length;
            int unitIndex_high = (inputLen - 1) / 4;
            int unitIndex_low = (inputLen - 1) % 4;
            StringBuilder stbr = new StringBuilder();
            bool naZero = false;
            bool noNuminthisunit = true;

            for(int i = 0;i < inputLen;i++)
            {
                // 先讀該位數字
                char c = num[i];
                if(c > '0')
                {
                    if(naZero)
                    {
                        stbr.Append(chineseNum[0]);
                        naZero = false;
                    }
                    stbr.Append(chineseNum[c - '0']);
                    noNuminthisunit = false;
                }
                else
                {
                    // 當該位數字為 0 時，先註記 0，有可能連續多位為0
                    // 但當高位單位切換時，要判斷是否需要補上單位
                    // 如果此高位單位中所有數字皆0，則不補；若有，則需要補
                    // 1000000500 = 拾億零伍佰，不用補「萬」
                    // 1002000500 = 拾億零貳佰萬零伍佰，需補「萬」
                    naZero = true;

                    if (unitIndex_low == 0)
                    {
                        unitIndex_low = 3;
                        if(!noNuminthisunit)
                        {
                            stbr.Append(chineseUnit_high[unitIndex_high]);
                            naZero = false;
                            noNuminthisunit = true;
                        }
                            
                        unitIndex_high--;
                    }
                    else
                        unitIndex_low--;
                    continue;
                }

                // 再讀單位
                stbr.Append(chineseUnit_low[unitIndex_low]);
                if (unitIndex_low == 0)
                {
                    unitIndex_low = 3;
                    stbr.Append(chineseUnit_high[unitIndex_high]);
                    unitIndex_high--;
                    noNuminthisunit = true;
                }
                else
                    unitIndex_low--;
            }
            return stbr.ToString();
        }
    }
}
