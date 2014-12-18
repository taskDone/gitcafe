﻿using LibGit2Sharp.Handlers;

namespace LibGit2Sharp
{
    /// <summary>
    /// Base collection of parameters controlling Fetch behavior.
    /// </summary>
    public abstract class FetchOptionsBase
    {
        internal FetchOptionsBase()
        {
        }

        /// <summary>
        /// Handler for network transfer and indexing progress information.
        /// </summary>
        public ProgressHandler OnProgress { get; set; }

        /// <summary>
        /// Handler for updates to remote tracking branches.
        /// </summary>
        public UpdateTipsHandler OnUpdateTips { get; set; }

        /// <summary>
        /// Handler for data transfer progress.
        /// <para>
        /// Reports the client's state regarding the received and processed (bytes, objects) from the server.
        /// </para>
        /// </summary>
        public TransferProgressHandler OnTransferProgress { get; set; }

        /// <summary>
        /// Handler to generate <see cref="LibGit2Sharp.Credentials"/> for authentication.
        /// </summary>
        public CredentialsHandler CredentialsProvider { get; set; }
    }
}
