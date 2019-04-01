using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ToDoListServer.Models
{
    /// <summary>
    /// An item on the ToDo list
    /// </summary>
    [DataContract]
    public class Item
    {
        /// <summary>
        /// ID of user who is placing item on the list
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public string UserID { get; set; }

        /// <summary>
        /// Description of item
        /// </summary>
        public string Description { get; set; }
    }
}