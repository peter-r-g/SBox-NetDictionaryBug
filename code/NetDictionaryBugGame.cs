using System.Collections.Generic;
using Sandbox;
using Sandbox.UI;

namespace NetDictionaryBug
{
	partial class NetDictionaryBugGame : GameBase
	{
		[Net] public IDictionary<int, UnusableClass> BugHere { get; private set; }
		
		public NetDictionaryBugGame()
		{
			if ( IsServer )
				_ = new NetDictionaryBugHud();
		}

		public override void Simulate( Client cl )
		{
			if ( IsServer && Input.Pressed( InputButton.Reload ) )
				BugHere.Add( BugHere.Count, new UnusableClass() );
		}
		
		internal class UnusableClass
		{
			// Any class no matter what it has inside of it will not work.
		}

		// Forced to define these :\
		public override CameraSetup BuildCamera( CameraSetup camSetup ) => camSetup;
		public override void OnVoicePlayed( long playerId, float level ) { }
		public override bool CanHearPlayerVoice( Client source, Client dest ) => true;
		public override void ClientDisconnect( Client cl, NetworkDisconnectionReason reason ) { }
		public override void ClientJoined( Client cl ) { }
		public override void PostLevelLoaded() { }
		public override void Shutdown() { }
	}

	internal class NetDictionaryBugHud : HudEntity<RootPanel>
	{
		public NetDictionaryBugHud()
		{
			if ( !IsClient )
				return;

			RootPanel.SetTemplate( "/hud.html" );
		}
	}
}
