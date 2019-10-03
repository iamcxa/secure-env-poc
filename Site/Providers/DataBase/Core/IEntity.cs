using System;

namespace Demo.Providers.DataBase
{
    public interface IEntity
    {
        int SN { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifyDate { get; set; }
        bool IsDeleted { get; set; }
    }

}
