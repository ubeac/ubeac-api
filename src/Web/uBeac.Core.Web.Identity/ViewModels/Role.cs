using System;
using System.ComponentModel.DataAnnotations;

namespace uBeac.Web.Identity
{
    public class RoleAddRequest
    {
        [Required]
        public string Name { get; set; }
    }

    public class RoleUpdateRequest<TKey> where TKey : IEquatable<TKey>
    {
        [Required]
        public TKey Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
    public class RoleUpdateRequest : RoleUpdateRequest<Guid>
    {
    }

    public class RoleResponse<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public string Name { get; set; }
    }

    public class RoleResponse : RoleResponse<Guid>
    {
    }
}
