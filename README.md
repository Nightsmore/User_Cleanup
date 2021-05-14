# User Cleanup | C# Application to quickly delete user profiles


This application works retreving profile data from both the Registry &'c:\users\' folder, then whichever is selected for deletion, it recurisively retreives all directories and files, adjusting permissions and then removing that contnet. 



#
<h3>:warning:  Delete profiles with caution! :warning:</h3>  

<i>This requires administrative permssions and will strip folder security permissions to delete. This will also delete registry values 
associated to whichever profile you select. 
Currently there's no warnings or markers indicating what is purely a registry or system item, there's no undoing the removal either. </i>
#
