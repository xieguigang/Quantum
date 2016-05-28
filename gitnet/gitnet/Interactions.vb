
''' <summary>
''' The interaction network between the objects on the github
''' </summary>
Public Enum Interactions

    ''' <summary>
    ''' On user following another user 
    ''' </summary>
    UserFollowingUser
    ''' <summary>
    ''' User folk a repository
    ''' </summary>
    UserFolkRepo
    ''' <summary>
    ''' User star a repository
    ''' </summary>
    UserStarRepo
    ''' <summary>
    ''' User is the member of a organization 
    ''' </summary>
    OrgsMember
    ''' <summary>
    ''' Organization folk a repository 
    ''' </summary>
    OrgFolkRepo
    ''' <summary>
    ''' This is a user repository 
    ''' </summary>
    UserRepo
    ''' <summary>
    ''' This is a organization repository 
    ''' </summary>
    OrgsRepo
End Enum
