﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Timers;
using System.Windows;
using Timer = System.Timers.Timer;
using ICSharpCode;
using ICSharpCode.SharpZipLib;
using System.IO;
using System.Reflection;
using Id3Lib;
using Mp3Lib;
namespace MediaPlayer
{

    class MainController
    {
        private MainWindow _view;
        private int _selectedMedia;
        private int _volume; // 1 - 100
        private PlayModeEnum _playMode;
        private MediaElement _mediaElement;
        private Timer _mediaElementPollingTimer;
        private DatabaseControllerStub _databaseController;
        private FileScannerStub _fileScanner;
        private Thread _fileScannerThread;
        private List<string> _supportedExtentions;
        
        public List<string> SupportedExtentions { get { return _supportedExtentions; } } 

        public MainController(MainWindow view)
        {
            _view =  view;
            _supportedExtentions = new List<string>() {"*mp3", "*m4a", "*mp4"};
        }

        public void Setup()
        {
            _mediaElement = _view.mediaElement;
            _mediaElementPollingTimer = new Timer(500); // Create a timer that polls every 1/2 second
            _mediaElementPollingTimer.Elapsed += new ElapsedEventHandler(PollingTimerHandler);
            _mediaElementPollingTimer.Start();

            _databaseController = new DatabaseControllerStub();
            _fileScanner = new FileScannerStub(this);

            _fileScannerThread = new Thread(new ParameterizedThreadStart(_fileScanner.ScanDirectory ));
            _fileScannerThread.Start("C:\\");
            /**
             *   TODO:
             *      1) load serialized state from savedState.cfg (in current directory)
             *         - If error, setup default state and create/correct savedState.cfg
             *     
             *      2) Instantiate database controller
             *         - Check to see of if there is a media library database, if not create one.
             *
             *      3) Check to see if there are records in the database
             *         a) There are records: 
             *            - Populate the media library control (the DataGridView)
             *            - Load the last selected song as described in savedState.cfg (if the song is in the library)
             *         b) There are no records: 
             *            - Scan the entire file system for media files, as files are found, update the view as appropriate.           
             *
             *      2) Load the playlist data from the file ??????????? and update the view appropiately, 
             *           
             *      3) st 
             */
        }


        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// </returns>
        private bool loadSavedState()
        {
            // TODO: Read in state data from savedState.cfg and use it to set the state.
            return false;
        }

        public void PollingTimerHandler(object sender, ElapsedEventArgs e)
        {
            CheckMediaObjectStatus();
        }

        public void CheckMediaObjectStatus()
        {
            Console.WriteLine("Check Status");
            if (_mediaElement != null)
            {
                // TODO: Update the view (the progress bar and timer)
                //       Check to see if the media has finished, in which case move on the the next song (using the current play mode)
            }

        }

        
        public void DeleteLibraryEntry() { }

        // This method is called by the FileScanner 
        // (or possibly another piece of code) when a new file needs to be added to the library
        public void AddMediaEvent(string newMediaPath)
        {
            //Console.WriteLine("Add media file: " + newMediaPath);
            Dictionary<string, string> tags = TagManager.GetMediaTags(newMediaPath);
            _databaseController.addToLibrary(newMediaPath, tags["filename"], tags["title"], tags["duration"], tags["artist"], tags["album"]);
        }

        public void FetchMediaLibraryData() { }

        #region View Events
        public void PlayButtonPressed() { Console.WriteLine("Play");}
        public void StopButtonPressed() { Console.WriteLine("Stop");}
        public void PauseButtonPressed() { Console.WriteLine("Pause");}
        public void VolumeChanged() { Console.WriteLine("Volume");}
        public void ProgressBarMovedByUser() { Console.WriteLine("ProgressBar");}

        public void CloseWindow()
        {
            Console.WriteLine("Close");
            _fileScannerThread.Abort();
            _mediaElementPollingTimer.Stop();
            _mediaElementPollingTimer.Close();
        }
        public void MediaCompleted() { }
        public void MediaFileError() { }
        #endregion View Events 



    }
}