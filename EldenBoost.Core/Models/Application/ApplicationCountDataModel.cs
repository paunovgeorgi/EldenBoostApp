using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Core.Models.Application
{
	public class ApplicationCountDataModel
	{
        public int BoosterPending { get; set; }
        public int BoosterApproved { get; set; }
        public int AuthorPending { get; set; }
        public int AuthorApproved { get; set; }
    }
}
