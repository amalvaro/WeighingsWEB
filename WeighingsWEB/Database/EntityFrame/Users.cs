using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class Users
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] Salt { get; set; }

        public bool IsDeleted { get; set; }

    }
}
