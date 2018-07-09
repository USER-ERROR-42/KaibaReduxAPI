using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaibaReduxAPI.Models
{
    public class Section
        // Represents a menu section, like appetizers or desserts
    {
        // class variables
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // this number represents the position for this section within the menu
        // ie. Is this section first, fifth, or seventh
        public float Position { get; set; }

        // this holds the path to the picture for this section
        public string PicturePath { get; set; }

        // the list of items in this section
        public List<Item> ItemList { get; set; }

        public Section(int id, string name, string description, float position, string picturePath)
        // Standard constructor- takes and assigns the class variables
        {
            Id = id;
            Name = name;
            Description = description;
            Position = position;
            PicturePath = picturePath;
            ItemList = new List<Item>();
        }

        public void addItem(Item item)
        {
            ItemList.Add(item);
        }

        public List<Item> getItemList()
        {
            return ItemList;
        }
    }
}
