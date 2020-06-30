using System;

namespace ModuloLog
{
    /// <summary>
    /// Interface for managing data logging
    /// </summary>
    /// <typeparam name="T">Target of data to be written (StreamWriter, File, Logger, etc)</typeparam>
    public interface ILogging<T>
    {
        /// <summary>
        /// Method for creating a reference to a file (which could be a StreamWriter, File, Logger, etc)
        /// </summary>
        /// <param name="name">Name of the file to be created</param>
        /// <param name="directory">Directory where the file is created</param>
        /// <returns>Returns a reference to the file</returns>
        T CreateLogTarget(string name, string directory);

        /// <summary>
        /// Method to write on a file previously created
        /// </summary>
        /// <param name="file">Reference to the file to be written upon</param>
        /// <param name="contents">Contents to be written on the file</param>
        /// <returns>True if written correctly, False if an exception occurs</returns>
        bool WriteToLog(T file, string contents);

        /// <summary>
        /// Method to close a file and free resources associated with it
        /// </summary>
        /// <param name="file">Reference to the file to be closed</param>
        /// <returns>True if closed correctly, False if an exception occurs</returns>
        bool CloseLogTarget(T file);

    }
}
