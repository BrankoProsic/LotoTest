using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotoTest
{
    public class LotteryCombination
    {
        public LotteryCombination() { }
        public int CombinationId { get; set; }
        
        //[NotMapped]
        public List<int> Numbers { get; set; }
        public DateTime DrawDate { get; set; }

        //public LotteryCombination(int combinationID, List<int> numbers, DateTime drawDate)
        //{
        //    CombinationId = combinationID;
        //    Numbers = numbers;
        //    DrawDate = drawDate;
        //}
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static LotteryCombination FromJson(string json)
        {
            return JsonConvert.DeserializeObject<LotteryCombination>(json);
        }
    }
}
