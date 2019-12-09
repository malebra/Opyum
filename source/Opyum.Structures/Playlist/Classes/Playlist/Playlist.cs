using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Opyum.Structures.Playlist
{
    public class Playlist : IEnumerable, IDisposable, ICollection<IPlaylistItem>, IXmlSerializable
    {
        public LinkedList<IPlaylistItem> ItemPlaylist { get; protected set; } = new LinkedList<IPlaylistItem>();


        
        public IPlaylistItem Next { get; protected internal set; }

        public IPlaylistItem First => throw new NotImplementedException();


        public Playlist()
        {
            
        }




        #region Garbage collection

        public void Dispose()
        {
            Dispose(true);
            GC.Collect(); 
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
        }

        #endregion


        /*
        #region Stack



        


        public override void Clear()
        {
            ItemPlaylist = new LinkedList<IPlaylistItem>();
            GC.Collect();
        }

        public override object Clone()
        {
            throw new NotImplementedException(); 
        }

        public override bool Contains(object obj) => typeof(IPlaylistItem).IsAssignableFrom(obj.GetType()) ? ItemPlaylist.Contains((IPlaylistItem)obj) : false;

        public override void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }


        public override object Peek()
        {
            throw new NotImplementedException();
        }

        public override object Pop()
        {
            throw new NotImplementedException();
        }

        public override void Push(object obj)
        {
            throw new NotImplementedException();
        }

        public override object[] ToArray()
        {
            throw new NotImplementedException();
        }



        #endregion
        */

        #region IEnumerable

        public IEnumerator GetEnumerator() => (IEnumerator)ItemPlaylist;

        #endregion

        #region Collection

        public bool IsReadOnly => false;

        public int Count => ItemPlaylist.Count;

        public void Add(IPlaylistItem item)
        {
            ItemPlaylist.AddLast(item);
        }

        public void AddBefor(IPlaylistItem replacent, IPlaylistItem item)
        {
            ItemPlaylist.AddBefore(ItemPlaylist.Find(replacent), item);
        }

        public void AddAfter(IPlaylistItem replacent, IPlaylistItem item)
        {
            ItemPlaylist.AddAfter(ItemPlaylist.Find(replacent), item);
        }

        public bool Contains(IPlaylistItem item) => ItemPlaylist.Contains(item);

        public void CopyTo(IPlaylistItem[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IPlaylistItem item)
        {
            if (ItemPlaylist.Contains(item))
            {
                ItemPlaylist.Remove(item);
                return true;
            }
            return false;
        }

        IEnumerator<IPlaylistItem> IEnumerable<IPlaylistItem>.GetEnumerator() => (IEnumerator<IPlaylistItem>)ItemPlaylist;


        public void Clear()
        {
            ItemPlaylist = new LinkedList<IPlaylistItem>();
            GC.Collect();
        }


        #endregion

        #region Conversion

        public string ToJSON()
        {
            throw new NotImplementedException();
        }

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }






        #endregion

    }
}
