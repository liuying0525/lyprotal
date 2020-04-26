using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZAFCPortal.Console
{
    public interface IEntity
    {
        string ID { get; set; }

        [Column("INT_ID")]
        long Int_ID { get; set; }
    }
}
