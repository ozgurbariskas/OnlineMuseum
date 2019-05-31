using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.WebMVC.Models
{
    public static class ExhibitionRepository
    {
        static ExhibitionRepository() {
            exhibitions = new List<Exhibition>()
            {
                new Exhibition(){ id=1, price=5, name="Noongom", image="1.jpg",date = new DateTime(2019,4,20),description="This exhibition welcomes new works into our collection, helping Museum London convey a broader representation of contemporary art created by Indigenous artists. These dynamic works illuminate Aboriginal history and lived experience, engage in difficult conversations about injustice, and push the boundaries of contemporary art practice in Canada and internationally."},
                new Exhibition(){ id=2, price=10, name="Difficult Terrain", image="2.jpg",date = new DateTime(2019,4,6),description="A collection of historic objects and images that spark a conversation to oppose prejudice, discrimination, and oppression as well as to promote inclusion and diversity." },
                new Exhibition(){ id=3, price=15, name="The Art of Music: A Student Exhibition", image="3.jpg",date = new DateTime(2019,1,12),description="Just in time for the JUNOS, this exhibition features approximately 100 juried works inspired by art and music by the elementary and secondary students of London." }

            };
        }


        private static List<Exhibition> exhibitions = new List<Exhibition>();
        
        public static List<Exhibition> getExhibitions() {  return exhibitions;   }

        public static void AddExhibition(Exhibition exhibition)
        {
            exhibitions.Add(exhibition);
        }

        public static Exhibition GetById(int id) {
            return exhibitions.FirstOrDefault(k => k.id == id);
        }

        public static void Delete(int id) {
            exhibitions.Remove(GetById(id));
        }

        public static int generateId()
        {
            int maxId = int.MinValue;
            foreach (Exhibition type in exhibitions)
            {
                int currentId = type.id;
                if (currentId > maxId)
                {
                    maxId = currentId;
                }
            }
            return (maxId+1);
        }
    }
}
