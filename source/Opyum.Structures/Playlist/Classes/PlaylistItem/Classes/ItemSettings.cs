using System;

namespace Opyum.Playlist
{
    public enum LeewayOvershoot
    {
        /// <summary>
        /// The item will be set to the closest position based on the item start time and leeway.
        /// </summary>
        ClosestToLeeway = 0,
        /// <summary>
        /// Some other <see cref="PlaylistItem"/> will be adjusted to correctly place this item.
        /// </summary>
        ReplaceLinkedItem = 1,
        /// <summary>
        /// The linked <see cref="PlaylistItem"/> will be deleted so this item can be placed correctly in the list.
        /// </summary>
        DeleteLinkedItem = 2,
        /// <summary>
        /// The linked <see cref="PlaylistItem"/> will be deleted  or replaced so this item can be placed correctly in the list.
        /// </summary>
        ReplaceOrDeleteLinkedItem = 3,
        /// <summary>
        /// Will check if a jingle is set and:
        /// <para>1.) if a JINGLE is set then fade out the playling song, play JINGLE then play the Item,</para>
        /// <para>2.) if a JINGLE isn't set then fade out the playling song and play the Item. </para>
        /// </summary>
        Fixed = 4
    }

    public enum LeewayOrientation
    {
        /// <summary>
        /// Not set.
        /// </summary>
        None = 0,
        /// <summary>
        /// Before the selected time
        /// </summary>
        Before = 1,
        /// <summary>
        /// After the selected item
        /// </summary>
        After = 2,
        /// <summary>
        /// Both before and after.
        /// </summary>
        BeforeAndAfter = 3
    }

    public  class ItemSettings
    {

        /// <summary>
        /// The leeway amount for the item to find acceptable to overshoot or undershoot if the <see cref="TimeType"/> is set ot <see cref="TimeType.Set"/>.
        /// <para>This means that if the set time cannot be reached the item will try to arrange itself a leeway amount above or below the set time.</para>
        /// <para>If event that cannot be achieved, aditional variables are taken into consideration, but either the closest timeframe above and/or below the set time will be selected,
        /// or other items will have to change their duration or be changed completly.</para>
        /// </summary>
        public TimeSpan Leeway { get; set; }
        /// <summary>
        /// Determins what shood be done if the item cannot be inserted in the selected leeway.
        /// </summary>
        public LeewayOvershoot Overshoot { get; set; }
        /// <summary>
        /// Determines if the leeway is being lookd at before and/or after the designated time for the item.
        /// </summary>
        public LeewayOrientation LeewayOritentation { get; set; }
        
        
        
        /// <summary>
        /// The items the settings are linked to.
        /// </summary>
        public PlaylistItem SettingsItem { get; private set; }



        /// <summary>
        /// The item that other settings may affect based on this items settings.
        /// </summary>
        public PlaylistItem LinkedItem { get; set; }




    }
}
