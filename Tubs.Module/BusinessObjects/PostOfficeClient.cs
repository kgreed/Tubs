using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo.DB;
using DevExpress.Xpo;
namespace Tubs.Module.BusinessObjects
{
    public class PostOfficeClient
    {
        static PostOfficeClient()
        {
            GlobalServiceProvider<PostOfficeClient>.AddService(() => new PostOfficeClient());
        }
        public Dictionary<Type, DataStoreMapping> Mappings { get; private set; }
        public IDataStore DataStore { get; private set; }
        public PostOfficeClient()
        {
             this.DataStore = new InMemoryDataStore(AutoCreateOption.DatabaseAndSchema, false);
            this.Mappings = new Dictionary<Type, DataStoreMapping>();
            var mAccount = new DataStoreMapping();
            mAccount.Table = new DBTable("Accounts");
            mAccount.Table.AddColumn(new DBColumn("UserName", true, null, 255, DBColumnType.String));
            mAccount.Table.AddColumn(new DBColumn("PublicName", false, null, 1024, DBColumnType.String));
            mAccount.Create = () => new NPTub();
            mAccount.Load = (obj, values, omap) =>
            {
                var o = ((NPObj) obj);
                o.Id = (int)values[0];
                o.Name = (string)values[1];
                //((NPTub)obj).SetKey((string)values[0]);
                //((Account)obj).PublicName = (string)values[1];
            };
            mAccount.Save = (obj, values) =>
            {
                values[0] = ((NPObj)obj).Id;
                values[1] = ((NPObj)obj).Name;
            };
            mAccount.GetKey = (obj) => ((Account)obj).UserName;
            mAccount.RefColumns = Enumerable.Empty<DataStoreMapping.Column>();
            Mappings.Add(typeof(Account), mAccount);
            var mMessage = new DataStoreMapping();
            mMessage.Table = new DBTable("Messages");
            var mMessageKey = new DBColumn("ID", true, null, 0, DBColumnType.Int32);
            mMessageKey.IsIdentity = true;
            mMessage.Table.AddColumn(mMessageKey);
            mMessage.Table.AddColumn(new DBColumn("Subject", false, null, 1024, DBColumnType.String));
            mMessage.Table.AddColumn(new DBColumn("Body", false, null, -1, DBColumnType.String));
            mMessage.Table.AddColumn(new DBColumn("Sender", false, null, 255, DBColumnType.String));
            mMessage.Table.AddColumn(new DBColumn("Recepient", false, null, 255, DBColumnType.String));
            mMessage.Table.PrimaryKey = new DBPrimaryKey(new object[] { mMessageKey });
            mMessage.Create = () => new Message();
            mMessage.SetKey = (obj, key) =>
            {
                ((Message)obj).SetKey((int)key);
            };
            mMessage.GetKey = (obj) => ((Message)obj).ID;
            mMessage.Load = (obj, values, omap) =>
            {
                var o = (Message)obj;
                o.SetKey((int)values[0]);
                o.Subject = (string)values[1];
                o.Body = (string)values[2];
                o.Sender = GetReference<Account>(omap, values[3]);
                o.Recepient = GetReference<Account>(omap, values[4]);
            };
            mMessage.Save = (obj, values) =>
            {
                var o = (Message)obj;
                values[0] = o.ID;
                values[1] = o.Subject;
                values[2] = o.Body;
                values[3] = o.Sender?.UserName;
                values[4] = o.Recepient?.UserName;
            };
            mMessage.RefColumns = new DataStoreMapping.Column[] {
                new DataStoreMapping.Column(){ Index = 3, Type = typeof(Account) },
                new DataStoreMapping.Column(){ Index = 4, Type = typeof(Account) }
            };
            Mappings.Add(typeof(Message), mMessage);
            DataStore.UpdateSchema(false, mAccount.Table, mMessage.Table);
            CreateDemoData((InMemoryDataStore)DataStore);
        }
        private static T GetReference<T>(ObjectMap map, object key)
        {
            return (key == null) ? default(T) : map.Get<T>(key);
        }

        public class GenHelper
        {
            private static Random srnd;
            private static List<string> words;
            private static List<string> fnames;
            private static List<string> lnames;
            static GenHelper()
            {
                srnd = new Random();
                words = CreateWords(12000);
                fnames = CreateNames(200);
                lnames = CreateNames(500);
            }
            private static List<string> CreateWords(int number)
            {
                var items = new HashSet<string>();
                while (number > 0)
                {
                    if (items.Add(CreateWord()))
                    {
                        number--;
                    }
                }
                return items.ToList();
            }
            private static string MakeTosh(Random rnd, int length)
            {
                var chars = new char[length];
                for (int i = 0; i < length; i++)
                {
                    chars[i] = (char)('a' + rnd.Next(26));
                }
                return new String(chars);
            }
            private static string CreateWord()
            {
                return MakeTosh(srnd, 1 + srnd.Next(13));
            }
            private static List<string> CreateNames(int number)
            {
                var items = new HashSet<string>();
                while (number > 0)
                {
                    if (items.Add(ToTitle(CreateWord())))
                    {
                        number--;
                    }
                }
                return items.ToList();
            }
            public static string ToTitle(string s)
            {
                if (string.IsNullOrEmpty(s))
                    return s;
                return string.Concat(s.Substring(0, 1).ToUpper(), s.Substring(1));
            }

            private Random rnd;
            public GenHelper()
            {
                rnd = new Random();
            }
            public GenHelper(int seed)
            {
                rnd = new Random(seed);
            }
            public int Next(int max)
            {
                return rnd.Next(max);
            }
            public string MakeTosh(int length)
            {
                return MakeTosh(rnd, length);
            }
            public string MakeBlah(int length)
            {
                var sb = new StringBuilder();
                for (var i = 0; i <= length; i++)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                    }
                    sb.Append(GetWord());
                }
                return sb.ToString();
            }
            public string MakeBlahBlahBlah(int length, int plength)
            {
                var sb = new StringBuilder();
                for (var i = 0; i <= length; i++)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                    }
                    var w = ToTitle(MakeBlah(3 + rnd.Next(plength))) + ".";
                    sb.Append(w);
                }
                return sb.ToString();
            }
            public string GetFullName()
            {
                return string.Concat(GetFName(), " ", GetLName());
            }
            private string GetFName()
            {
                return fnames[rnd.Next(fnames.Count)];
            }
            private string GetLName()
            {
                return lnames[rnd.Next(lnames.Count)];
            }
            private string GetWord()
            {
                return words[rnd.Next(words.Count)];
            }
        }
        //#region Demo Data
        //static void CreateDemoData(InMemoryDataStore inMemoryDataStore)
        //{
        //    var ds = new System.Data.DataSet();
        //    using (var ms = new System.IO.MemoryStream())
        //    {
        //        using (var writer = System.Xml.XmlWriter.Create(ms))
        //        {
        //            inMemoryDataStore.WriteXml(writer);
        //            writer.Flush();
        //        }
        //        ms.Flush();
        //        ms.Position = 0;
        //        ds.ReadXml(ms);
        //    }
        //    var gen = new GenHelper();
        //    var idsAccount = new List<string>();
        //    var dtAccounts = ds.Tables["Accounts"];
        //    for (int i = 0; i < 200; i++)
        //    {
        //        var id = gen.MakeTosh(20);
        //        idsAccount.Add(id);
        //        dtAccounts.Rows.Add(id, gen.GetFullName());
        //    }
        //    var dtMessages = ds.Tables["Messages"];
        //    for (int i = 0; i < 5000; i++)
        //    {
        //        var id1 = gen.Next(idsAccount.Count);
        //        var id2 = gen.Next(idsAccount.Count - 1);
        //        dtMessages.Rows.Add(null, GenHelper.ToTitle(gen.MakeBlah(gen.Next(7))), gen.MakeBlahBlahBlah(5 + gen.Next(100), 7),
        //            idsAccount[id1], idsAccount[(id1 + id2 + 1) % idsAccount.Count]);
        //    }
        //    ds.AcceptChanges();
        //    using (var ms = new System.IO.MemoryStream())
        //    {
        //        ds.WriteXml(ms, System.Data.XmlWriteMode.WriteSchema);
        //        ms.Flush();
        //        ms.Position = 0;
        //        using (var reader = System.Xml.XmlReader.Create(ms))
        //        {
        //            inMemoryDataStore.ReadXml(reader);
        //        }
        //    }
        //}
        //#endregion
    }
}