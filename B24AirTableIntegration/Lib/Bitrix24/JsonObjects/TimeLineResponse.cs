using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace B24AirTableIntegration.Lib.Bitrix24
{
    public class TimeLineEntry : IEquatable<TimeLineEntry>
    {
        public string ID { get; set; }
        public int ENTITY_ID { get; set; }
        public string ENTITY_TYPE { get; set; }
        public DateTime CREATED { get; set; }
        public string COMMENT { get; set; }
        public string AUTHOR_ID { get; set; }

        private UserResponse authorUser = null;
        [Newtonsoft.Json.JsonIgnore]
        public UserResponse AuthorUser
        {
            get
            {
                if (authorUser == null)
                {
                    if (string.IsNullOrWhiteSpace(AUTHOR_ID))
                        return null;
                    authorUser = BitrixClient.Instance.GetUser(AUTHOR_ID);
                }
                return authorUser;
            }
            set
            {
                authorUser = value;
            }
        }

        public override string ToString()
        {
            return $"{CREATED:dd.MM} {COMMENT}";
        }

        public bool Equals(TimeLineEntry other)
        {
            return ID.Equals(other.ID);
        }
    }

    public class CommentResponse
    {
        [Newtonsoft.Json.JsonProperty("result")]
        public TimeLineEntry Comment { get; set; }
        public Time time { get; set; }
    }

    public class TimeLineResponse
    {
        [Newtonsoft.Json.JsonProperty("result")]
        public List<TimeLineEntry> Entries { get; set; }
        public int total { get; set; }
        public Time time { get; set; }
        
        public string LastComment
        {
            get
            {
                var lastEntry = Entries?.OrderByDescending(x => x.CREATED).FirstOrDefault();
                if (lastEntry != null)
                    return lastEntry.ToString();
                return null;
            }
        }
    }
}