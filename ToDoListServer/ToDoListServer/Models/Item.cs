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
        public string UserID { get; set; }

        /// <summary>
        /// For demo purposes
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public int Age { get; set; }

        /// <summary>
        /// Description of item
        /// </summary>
        public string Description { get; set; }
    }
}