﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer
{
    [DataContract]
    public class MediaItem
    {
        [DataMember]
        public string Album { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Year { get; set; }
        [DataMember]
        public string Artist { get; set; }
        // public string Id { get; set; }
        [DataMember]
        public string Genre { get; set; }
        [DataMember]
        public long Duration { get; set; }
        [DataMember]
        public string Filename { get; set; }
        [DataMember]
        public string Filetype { get; set; }
        [DataMember]
        public string Filepath { get; set; }
        [DataMember]
        public double Position { get; set; }
        [DataMember]
        public bool IsPlaying { get; set; }

        public MediaItem(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("An attempt was made to create a media item without a path");
            Filepath = path;
            IsPlaying = false;
            Filename = "";
            Position = 0.0;
            Duration = 0;
            Album = "";
            Title = "";
            Artist = "";
            Year = "";
            Genre = "";
        }

        public MediaItem()
        {
            Filepath = "";
            IsPlaying = false;
            Position = 0.0;
            Duration = 0;
            Album = "";
            Title = "";
            Artist = "";
            Year = "";
            Genre = "";
        }

        public override int GetHashCode()
        {
            if (String.IsNullOrEmpty(Filepath))
                return 0;

            return Filepath.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(string.IsNullOrEmpty(Filepath) || (obj.GetType() != this.GetType()))
                return false;

            MediaItem that = (MediaItem) obj;
            if (String.IsNullOrEmpty(that.Filepath))
                return false;
            return Filepath.Equals(((MediaItem)obj).Filepath);
        }

        public override string ToString()
        {
            return (string.IsNullOrEmpty(Filepath)) ? "" : Filepath.ToString();
        }

        public MediaItem Clone()
        {
            return new MediaItem(Filepath)
            {
                Album = Album,
                Title = Title,
                Year = Year,
                Artist = Artist,
                Genre = Genre,
                Duration = Duration, 
                Filetype =  Filetype,
                Filepath = Filepath,
                IsPlaying = false,
                Position = 0.0
            };
        }
    }
}
