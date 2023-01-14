using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACW2
{
    public interface IListColorable
    {
        // return a list of objects that have a value less than 100
        List<ingredient> GetItems100();
        // return a list of object that have a value equal to0
        List<ingredient> GetItems0();
    }
}
