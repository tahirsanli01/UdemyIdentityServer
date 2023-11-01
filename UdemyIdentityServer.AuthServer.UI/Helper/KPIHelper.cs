using AdasoAdvisor.Models.ComponentViewDtos;
using Microsoft.AspNetCore.SignalR;

namespace AdasoAdvisor.Helper
{
    public static class KPIHelper
    {
        public static BriefCard2Dto Get()
        {
            List<BriefCard2Dto> list = new List<BriefCard2Dto>();
            list.Add(new BriefCard2Dto("İsdihdam", 500, CardBgColors.GetRandomColor()));
            list.Add(new BriefCard2Dto("İhracat Karbon Azalımı", 90, CardBgColors.GetRandomColor()));
            list.Add(new BriefCard2Dto("Enerji Tasarrufu", 10, CardBgColors.GetRandomColor()));
            list.Add(new BriefCard2Dto("Kullanılan Devlet Desteği", 3000000, CardBgColors.GetRandomColor()));
            list.Add(new BriefCard2Dto("SuTasarrufu", 3000, CardBgColors.GetRandomColor()));
            list.Add(new BriefCard2Dto("Atık Tasarrufu", 4000, CardBgColors.GetRandomColor()));
            list.Add(new BriefCard2Dto("OEE",2000, CardBgColors.GetRandomColor()));
            list.Add(new BriefCard2Dto("Karlılık Artışı", 500000, CardBgColors.GetRandomColor()));
            list.Add(new BriefCard2Dto("Yeni Pazar/Müşteri", 1200, CardBgColors.GetRandomColor()));

            Random rnd = new Random();
            return list[rnd.Next() % 8];
        }
    }
}
