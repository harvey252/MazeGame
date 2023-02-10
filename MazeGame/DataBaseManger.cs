using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MazeGame
{
    public static class DataBaseManger
    {

        //score class holds score enties
        [XmlInclude(typeof(Score))]
        public class Score : IComparable<Score>
        {
            public string name;
            public int mazes;
            public double time;
            public Score(string enterName, int enterMazes, double entertime)
            {
                name = enterName; mazes = enterMazes; time = entertime;

            }
            public Score()
            {

            }

            //used to compare when storting a list
            public int CompareTo(Score obj)
            {
                if (this.time > obj.time)
                {
                    return 1;
                }
                else if (this.time < obj.time)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }

        //gets score from XML
        private static List<Score> GetScores()
        {
            //conecting to XML
            XmlSerializer reader = new XmlSerializer(typeof(List<Score>), new Type[] { typeof(Score) });
            System.IO.StreamReader file = new System.IO.StreamReader("./../../../scores.xml");

            List<Score> scores = (List<Score>)reader.Deserialize(file);
            file.Close();
            return scores;
        }
        //saves score
        private static void SaveScores(List<Score> scores)
        {
            //conecting to XML
            XmlSerializer writer = new XmlSerializer(typeof(List<Score>), new Type[] { typeof(Score) });


            var path = "./../../../scores.xml";
            System.IO.FileStream file = System.IO.File.Create(path);
            writer.Serialize(file, scores);
            file.Close();

        }
        
        //returns top score
        public static Score GetTop()
        {
            List<Score> scores = GetScores();

            return scores[0];
        }

        //returns all scores (for outside calling)
        public static List<Score> GetAll()
        {
            return GetScores();
        }

        //add new entry to score
        public static void Add(string enterName, int enterMazes, double entertime)
        {
            List<Score> scores = GetScores();
           
            scores.Add(new Score(enterName, enterMazes, entertime));
            scores.Sort();
            scores.Reverse();
            if(scores.Count>10)
            {
                scores.RemoveAt(scores.Count - 1);
            }
            SaveScores(scores);
        }

    }

 


}
