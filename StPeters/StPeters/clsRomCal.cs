using System;

namespace StPeters
{
    class RomCal
    {
        //Based on rules for General Roman Calendar for RC Liturgical Calendar
        public Season SeasonOf(DateTime dteToFind)
        {

            int iYear = dteToFind.Year;
            Season[] SeasonList = new Season[19];

            SeasonList[0] = GetAdvent(iYear);
            SeasonList[1] = GetChristmasDec(iYear);
            SeasonList[2] = GetChristmasJan(iYear);
            SeasonList[3] = GetEasterSeason(iYear);
            SeasonList[4] = GetPentecost(iYear);
            SeasonList[5] = GetLent(iYear);
            SeasonList[6] = GetPalmSunday(iYear);
            SeasonList[7] = GetHolyWeek(iYear);
            SeasonList[8] = GetFirstOrdinaryTime(iYear);
            SeasonList[9] = GetBapLordSeason(iYear);
            SeasonList[10] = GetTrinitySunday(iYear); //must check before second ordinary!
            SeasonList[11] = GetCorpusChristi(iYear); //must check before second ordinary!
            SeasonList[12] = GetChristTheKing(iYear); //must check before second ordinary!
            SeasonList[13] = GetAllSaints(iYear); //must check before second ordinary!
            SeasonList[14] = GetAllSouls(iYear); //must check before second ordinary!
            SeasonList[15] = GetSecondOrdinaryTime(iYear);
            SeasonList[16] = GetHolySaturday(iYear);
            SeasonList[17] = GetGoodFriday(iYear);
            SeasonList[18] = GetHolyThursday(iYear);

            foreach (Season chkSeason in SeasonList)
            {
                if (chkSeason.InRange(dteToFind))
                {
                    return chkSeason;
                }
            }

            //should not return this if above correct!
            Season Undefined = new Season(DateTime.Now, DateTime.Now, "Undefined", Season.Colors.Green);
            return Undefined;

        } //function SeasonOf

        public DateTime GetEaster(int iYear)
        {
            // formula source: http://www.irt.org/articles/js052/
            double dblYear = iYear;
            double dblCentury = Math.Floor(dblYear / 100);
            var N = dblYear - 19 * Math.Floor(dblYear / 19);
            var K = Math.Floor((dblCentury - 17) / 25);
            var I = dblCentury - Math.Floor(dblCentury / 4) - Math.Floor((dblCentury - K) / 3) + 19 * N + 15;
            I = I - 30 * Math.Floor((I / 30));
            I = I - Math.Floor(I / 28) * (1 - Math.Floor(I / 28) * Math.Floor(29 / (I + 1)) * Math.Floor((21 - N) / 11));
            var J = dblYear + Math.Floor(dblYear / 4) + I + 2 - dblCentury + Math.Floor(dblCentury / 4);
            J = J - 7 * Math.Floor(J / 7);
            var L = I - J;
            double dblMonth = (3 + Math.Floor((L + 40) / 44));
            double dblDay = (L + 28 - 31 * Math.Floor(dblMonth / 4));

            DateTime dteReturn = new DateTime(iYear, (int)dblMonth, (int)dblDay);
            return dteReturn;
        } //GetEaster

        public DateTime GetBaptismOfLord(int iYear)
        {
            DateTime dteIndex;
            DateTime dteEpiphany = new DateTime(iYear, 1, 6);
            dteIndex = dteEpiphany;
            if (dteIndex.DayOfWeek == DayOfWeek.Sunday)
            {
                dteIndex = dteEpiphany.AddDays(1);
            }
            else
            {
                while (dteIndex.DayOfWeek != DayOfWeek.Sunday)
                {
                    dteIndex = dteIndex.AddDays(1);
                }
            }
            return dteIndex;
        } //GetBaptismOfLord

        public DateTime GetFirstSundayAdvent(int iYear)
        {
            DateTime dteChristmas;
            DateTime dteAdSunday;
            int iAdjust = 0;

            dteChristmas = new DateTime(iYear, 12, 25);
            if (dteChristmas.DayOfWeek == DayOfWeek.Sunday) iAdjust = -7;
            dteAdSunday = dteChristmas.AddDays(-21 + iAdjust);
            while (dteAdSunday.DayOfWeek != DayOfWeek.Sunday)
            {
                dteAdSunday = dteAdSunday.AddDays(-1);
            }
            return dteAdSunday;

        } //GetFirstSundayAdvent

        public Season GetAllSaints(int iYear)
        {
            Season AllSaintsDay;
            DateTime dteAllSaintsDay;

            dteAllSaintsDay = new DateTime(iYear, 11, 1);
            AllSaintsDay = new Season(dteAllSaintsDay, dteAllSaintsDay.AddDays(1), "All Saints Day", Season.Colors.White);
            return AllSaintsDay;
        }

        public Season GetAllSouls(int iYear)
        {
            Season AllSoulsDay;
            DateTime dteAllSoulsDay;

            dteAllSoulsDay = new DateTime(iYear, 11, 2);
            AllSoulsDay = new Season(dteAllSoulsDay, dteAllSoulsDay.AddDays(1), "All Souls Day", Season.Colors.White);
            return AllSoulsDay;
        }

        public Season GetChristTheKing(int iYear)
        {
            Season ChristTheKing;
            DateTime dteChristTheKing;

            dteChristTheKing = GetFirstSundayAdvent(iYear).AddDays(-7);
            ChristTheKing = new Season(dteChristTheKing, dteChristTheKing.AddDays(1), "Christ The King", Season.Colors.White);
            return ChristTheKing;
        }

        public Season GetCorpusChristi(int iYear)
        {
            Season CorpusChristi;
            DateTime dteCorpusChristi;

            dteCorpusChristi = GetEaster(iYear).AddDays(63);
            CorpusChristi = new Season(dteCorpusChristi, dteCorpusChristi.AddDays(1), "Corpus Christi", Season.Colors.White);
            return CorpusChristi;
        }

        public Season GetTrinitySunday(int iYear)
        {
            Season TrinitySunday;
            DateTime dteTrinitySun;

            dteTrinitySun = GetEaster(iYear).AddDays(56);
            TrinitySunday = new Season(dteTrinitySun, dteTrinitySun.AddDays(1), "Trinity Sunday", Season.Colors.White);
            return TrinitySunday;
        }

        public Season GetHolyThursday(int iYear)
        {
            Season HolyThurs;
            DateTime dteHolyThu;

            dteHolyThu = GetEaster(iYear).AddDays(-3);

            HolyThurs = new Season(dteHolyThu, dteHolyThu.AddDays(1), "Holy Thursday", Season.Colors.White);
            return HolyThurs;
        }

        public Season GetGoodFriday(int iYear)
        {
            Season GoodFriday;
            DateTime dteGoodFri;

            dteGoodFri = GetEaster(iYear).AddDays(-2);

            GoodFriday = new Season(dteGoodFri, dteGoodFri.AddDays(1), "Good Friday", Season.Colors.Red);
            return GoodFriday;
        }

        public Season GetHolySaturday(int iYear)
        {
            Season HolySaturday;
            DateTime dteHolySat;

            dteHolySat = GetEaster(iYear).AddDays(-1);

            HolySaturday = new Season(dteHolySat, dteHolySat.AddDays(1), "Holy Saturday", Season.Colors.White);
            return HolySaturday;
        }

        public Season GetSecondOrdinaryTime(int iYear)
        {
            Season SecondOrdinary;
            DateTime dtePentecost;
            DateTime dteAdvent;

            dtePentecost = GetEaster(iYear).AddDays(49);
            dteAdvent = GetFirstSundayAdvent(iYear);

            SecondOrdinary = new Season(dtePentecost, dteAdvent, "Ordinary Time", Season.Colors.Green);
            return SecondOrdinary;
        }

        public Season GetFirstOrdinaryTime(int iYear)
        {
            Season FirstOrdinary;
            DateTime dteBaptism;
            DateTime dteAshWed;

            dteBaptism = GetBaptismOfLord(iYear).AddDays(1);
            dteAshWed = GetEaster(iYear).AddDays(-46);

            FirstOrdinary = new Season(dteBaptism, dteAshWed, "Ordinary Time", Season.Colors.Green);
            return FirstOrdinary;
        }

        public Season GetHolyWeek(int iYear)
        {
            Season HolyWeek;
            DateTime dtePalmSun;
            DateTime dteHolyThursday;

            dtePalmSun = GetEaster(iYear).AddDays(-7);
            dteHolyThursday = GetEaster(iYear).AddDays(-3);

            HolyWeek = new Season(dtePalmSun, dteHolyThursday, "Holy Week", Season.Colors.Purple);
            return HolyWeek;
        }

        public Season GetLent(int iYear)
        {
            Season Lent;
            DateTime dtePalmSun;
            DateTime dteAshWed;

            dtePalmSun = GetEaster(iYear).AddDays(-7);
            dteAshWed = dtePalmSun.AddDays(-39);

            Lent = new Season(dteAshWed, dtePalmSun, "Lent", Season.Colors.Purple);
            return Lent;
        }

        public Season GetPalmSunday(int iYear)
        {
            Season PalmSunday;
            DateTime dteStartHolyWk;
            DateTime dtePalmSun;

            dteStartHolyWk = GetEaster(iYear).AddDays(-6);
            dtePalmSun = GetEaster(iYear).AddDays(-7);

            PalmSunday = new Season(dtePalmSun, dteStartHolyWk, "Palm Sunday", Season.Colors.Red);
            return PalmSunday;
        }

        public Season GetPentecost(int iYear)
        {
            Season Pentecost;
            DateTime dteEndEaster;
            DateTime dtePentecost;

            dteEndEaster = GetEaster(iYear).AddDays(49);
            dtePentecost = dteEndEaster.AddDays(1);

            Pentecost = new Season(dteEndEaster, dtePentecost, "Pentecost", Season.Colors.Red);
            return Pentecost;
        }

        public Season GetEasterSeason(int iYear)
        {
            Season EasterSeason;
            DateTime dteEaster;
            DateTime dtePentecost;

            dteEaster = GetEaster(iYear);
            dtePentecost = dteEaster.AddDays(49);

            EasterSeason = new Season(dteEaster, dtePentecost, "Easter", Season.Colors.White);
            return EasterSeason;

        } //GetEasterSeason

        public Season GetBapLordSeason(int iYear)
        {
            Season BaptismOfOurLord;
            DateTime dteBaptism;

            dteBaptism = GetBaptismOfLord(iYear);

            BaptismOfOurLord = new Season(dteBaptism, dteBaptism.AddDays(1), "Baptism of the Lord", Season.Colors.White);
            return BaptismOfOurLord;
        }

        public Season GetChristmasJan(int iYear)
        {
            Season ChristmasJan;
            DateTime dteChristmas;
            DateTime dteBaptism;

            dteChristmas = new DateTime(iYear, 01, 01);
            dteBaptism = GetBaptismOfLord(iYear);

            ChristmasJan = new Season(dteChristmas, dteBaptism, "Christmas", Season.Colors.White);
            return ChristmasJan;
        }

        public Season GetChristmasDec(int iYear)
        {
            Season ChristmasDec; //year will also have Xmas season in Jan
            DateTime dteChristmas;
            DateTime dteEndYr;

            dteChristmas = new DateTime(iYear, 12, 25);
            dteEndYr = new DateTime(iYear + 1, 01, 01);

            ChristmasDec = new Season(dteChristmas, dteEndYr, "Christmas", Season.Colors.White);
            return ChristmasDec;
        }

        public Season GetAdvent(int iYear)
        {
            Season Advent;
            DateTime dteAdSunday;
            DateTime dteChristmas;

            dteChristmas = new DateTime(iYear, 12, 25);
            dteAdSunday = GetFirstSundayAdvent(iYear);

            Advent = new Season(dteAdSunday, dteChristmas, "Advent", Season.Colors.Purple);
            return Advent;
        }

    } //class RomCal

    class Season
    {
        //holds season start and end dates for given year
        private DateTime dteStart;
        private DateTime dteEnd;
        private string strName;
        private Colors enumColor;

        public Season(DateTime start, DateTime end, string name, Season.Colors eColor)
        {
            dteStart = start;
            dteEnd = end;
            strName = name;
            enumColor = eColor;
        }

        public bool InRange(DateTime dteToFind)
        {
            if ((dteToFind >= dteStart) && (dteToFind < dteEnd))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string SeasonName
        {
            get
            {
                return strName;
            }
        }

        public Colors SeasonColor
        {
            get
            {
                return enumColor;
            }
        }

        public enum Colors
        {
            Green,
            Purple,
            Red,
            White
        }

    } //class Season

    class clsChurchYear
    {
        public clsChurchYear()
        { }

        public CHURCHYEAR GetChurchYear(System.DateTime dteToFind)
        {
            //Function to determine Year Cycle for RC Liturgical Calendar
            CHURCHYEAR eAnswer;

            try
            {
                int iYear = dteToFind.Year;
                int iSum = sumOfDigits(iYear);

                if (iSum % 3 == 0)
                {
                    //if div by 3 its year C
                    eAnswer = CHURCHYEAR.C;
                }
                else
                {
                    //check year before
                    iSum = (iSum - 1);
                    if (iSum % 3 == 0)
                    {
                        //its year A
                        eAnswer = CHURCHYEAR.A;
                    }
                    else eAnswer = CHURCHYEAR.B;
                }

                //Last - we need to handle december/lent which is new liturgical year:
                RomCal LitCal = new RomCal();
                //Console.WriteLine(LitCal.SeasonOf(dteToFind).SeasonName);

                if (dteToFind.Month == 12 | dteToFind.Month == 11)
                {
                    //then check if we are in new liturgical year:
                    if (LitCal.SeasonOf(dteToFind).SeasonName == "Advent")
                    {
                        //new year, need to increment
                        switch (eAnswer)
                        {
                            case CHURCHYEAR.A:
                                eAnswer = CHURCHYEAR.B;
                                break;

                            case CHURCHYEAR.B:
                                eAnswer = CHURCHYEAR.C;
                                break;

                            case CHURCHYEAR.C:
                                eAnswer = CHURCHYEAR.A;
                                break;
                        }
                    }
                }
                return eAnswer;
            }
            catch (Exception)
            {
                //we errored - shouldnt get here but rtn safe with invalid X
                return CHURCHYEAR.X;
            }
        } //GetChurchYear

        private int sumOfDigits(int num)
        {
            // Base condition
            if (num == 0)
                return 0;

            return ((num % 10) + sumOfDigits(num / 10));
        }

        public enum CHURCHYEAR
        {
            A, B, C, X
        }
    } //clsChurchYear

} //ns
