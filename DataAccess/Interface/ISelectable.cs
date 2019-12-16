using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    interface ISelectable
    {
        bool IsSelected { get; set; }
    }
}
