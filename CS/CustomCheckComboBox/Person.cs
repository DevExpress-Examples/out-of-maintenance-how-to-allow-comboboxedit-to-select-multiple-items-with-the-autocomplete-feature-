using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomCheckComboBox {
    class Person {
        public string Name { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public override string ToString() {
            return Name;
        }
    }
}
