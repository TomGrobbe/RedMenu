name 'RedMenu'
description 'Trainer / Menu created for the RedM community, useful for lots of things.'
author 'Vespura'
url 'https://github.com/tomgrobbe/redmenu/'
version 'v1.0.0'

-- Actual resource info setup stuff.
fx_version 'adamant' -- I like this version
games { 
    'rdr3' -- Read Documentation Really.... 3?
}

-- Files for the client side.
files {
    'MenuAPI.dll',
    'Newtonsoft.Json.dll',
}

-- Scripts
client_scripts {
    'RedMenuClient.net.dll'
}
server_scripts {
    'RedMenuServer.net.dll'
}

-- Yes this is an early build, I know.
rdr3_warning 'I acknowledge that this is a prerelease build of RedM, and I am aware my resources *will* become incompatible once RedM ships.'
