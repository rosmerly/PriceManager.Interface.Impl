using System;
using System.Collections.Generic;

interface IPriceManager
{
 void AddArticle(string name, double price, DateTime insertionDateTime);
 bool AddPromotion (string articleName, double discount, DateTime startDate, int durationDays);
 double GetPrice(string name, DateTime date);
 IEnumerable<string> GetArticleNames();
 void SetChristmasPeriod(DateTime startDate, DateTime endDate);
 void SetClearancePeriod(DateTime startDate, int DaysDuration);
}