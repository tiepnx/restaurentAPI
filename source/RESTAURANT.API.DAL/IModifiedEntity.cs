using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTAURANT.API.DAL
{
    //This interface is added to all the database entities
    //that have a modified date and rowGuid. Save Changes uses this
    // to find entities that need the date updating, or a new rowguid added
    public interface IModifiedEntity
    {
        System.Nullable<DateTime> Modified { get; set; }
        System.Nullable<DateTime> Created { get; set; }
        //Guid rowguid { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
        System.Nullable<Boolean> Deleted { get; set; }
    }

}
