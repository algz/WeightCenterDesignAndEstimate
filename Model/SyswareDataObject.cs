using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SyswareDataObject
    {
        private string _id;
        public string id
        {
            get
            {
                if (_id == null)
                {
                    _id = System.Guid.NewGuid().ToString();
                }
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string name
        {
            get;
            set;
        }

        public string value
        {
            get;
            set;
        }

        public string unit
        {
            get;
            set;
        }

        public string remark
        {
            get;
            set;
        }

        private List<SyswareDataObject> _children;
        public List<SyswareDataObject> children
        {
            get
            {
                if (_children == null)
                {
                    _children = new List<SyswareDataObject>();
                }
                return _children;
            }
            set
            {
                _children = value;
            }
        }
                   
    }
}
