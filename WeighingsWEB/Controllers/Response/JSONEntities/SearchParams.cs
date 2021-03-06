using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WeighingsWEB.Controllers.Response
{
    public class Date
    {
        public bool enable {get;set;}
        /// <summary>
        /// 
        /// </summary>
        
        public DateTime from { get; set; }    
        /// <summary>
        /// 
        /// </summary>
        public DateTime to { get; set; }
    }

    public class TypeAndStatus
    {
        
        /// <summary>
        /// 
        /// </summary>
        public byte? firstSelection { get; set; }    
        /// <summary>
        /// 
        /// </summary>
        public byte? secondSelection { get; set; }
    }

    public class CarNumber
    {
        
        /// <summary>
        /// 
        /// </summary>
        public string carNumber { get; set; }    
        /// <summary>
        /// 
        /// </summary>
        public bool fullContain { get; set; }
    }

    public class Directory
    {
        
        /// <summary>
        /// 
        /// </summary>
        // public int? firstSelection { get; set; }    
        /// <summary>
        /// 
        /// </summary>
        // public int? secondSelection { get; set; }

        public List<int?> selection;

    }

    public class Values
    {
        
        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }    
        /// <summary>
        /// 
        /// </summary>
        public int? selection { get; set; }    
        /// <summary>
        /// 
        /// </summary>
        public bool fullContain { get; set; }
    }

    public class SearchParams
    {
        
        /// <summary>
        /// 
        /// </summary>
        public Date date { get; set; }    
        /// <summary>
        /// 
        /// </summary>
        public TypeAndStatus typeAndStatus { get; set; }    
        /// <summary>
        /// 
        /// </summary>
        public CarNumber carNumber { get; set; }    
        /// <summary>
        /// 
        /// </summary>
        public Directory directory { get; set; }    
        /// <summary>
        /// 
        /// </summary>
        public Values values { get; set; }
    }
}