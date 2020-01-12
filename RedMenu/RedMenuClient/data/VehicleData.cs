using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedMenuClient.data
{
    class VehicleData
    {
        public static List<uint> vehicles = new List<uint>()
        {
            0x7f4258c9,
            0xc27964be,
            0xad516118,
            0xc8ef11a0,
            0x22c976e4,
            0x6a80d253,
            0x0cfd1449,
            0xef91537f,
            0x876e6eb7,
            0xecd7e90e,
            0x39584f5a,
            0x8c0224c6,
            0x578d6513,
            0xd84d4530,
            0xceded274,
            0x85943fe0,
            0xafb2141b,
            0xddfbf0ae,
            0x1656e157,
            0x0d10cecb,
            0x02d03a4a,
            0xe98507b4,
            0x68f6f8f3,
            0xf7d816b7,
            0xf775c720,
            0x0598b238,
            0x9324cd4e,
            0xa3ec6edd,
            0xb3c45542,
            0xbe696a8c,
            0x91068fc7,
            0x276dfe5e,
            0x5f27ed25,
            0x9780442f,
            0xec2a1018,
            0x9a308177,
            0xe89274f1,
            0x61ec29c0,
            0x69897b5c,
            0x1eebdbe5,
            0x2c3b0296,
            0x590420fe,
            0x75bddbd6,
            0x5eb0bae0,
            0x2ca8e7b6,
            0xc40b0265,
            0x9a0a187a,
            0x0f228f06,
            0x29ea09a9,
            0x930442ec,
            0x1c8d173a,
            0xc3e6c004,
            0x824ebbb5,
            0xf539e5a0,
            0xae057f07,
            0xb203c6b3,
            0xb31f8075,
            0x4717d8d8,
            0xdf86c25a,
            0x6f8f7ee4,
            0x0fd337b7,
            0x18296cde,
            0x4d5b5089,
            0xc50fc5d0,
            0x4e018632,
            0xf1fe5fb8,
            0x384e6422,
            0x22250ef5,
            0xee645446,
            0x055bf98f,
            0xf632a662,
            0x09d4e809,
            0x1c10e9c9,
            0x7724c788,
            0x20925d76,
            0x5e56769c,
            0x9fd6ba58,
            0xe84e6b74,
            0xf097bc6c,
            0xa385e1c7,
            0xe1fe4fd4,
            0x427a2d4c,
            0xc6fa5bff,
            0xdadc0b67,
            0x7dbbf975,
            0x680d3008,
            0xe7d930ea,
            0x9b58946e,
            0xc474677d,
            0xaafea8ae,
            0xda152ca6,
            0x7dd49b09,
            0xef1f4829,
            0x90c51372,
            0x6fbdd4b8,
            0x9735a3cf,
            0x3c9870a6,
            0xc2d200fe,
            0xee8254f6,
            0xf7e89a8d,
            0xccc649ae,
            0xe96cfefb,
            0x08ff91ed,
            0xa7fba623,
            0x8979274c,
            0x310a4f8b,
            0x0538529a,
            0xf53e4390,
        };
    }
    // Vehicle List in order of hashes

    /*privateopensleeper02x, Type: Train     
      <unknown>, Type: Train                 
      <unknown>, Type: Train                 
      <unknown>, Type: Train                 
      armoredcar03x, Type: Train             
      privatebaggage01X, Type: Train         
      SMUGGLER02, Type: Boat                 
      KEELBOAT, Type: Boat                   
      <unknown>, Type: Boat                   Small steam powered boat named Annie May
      <unknown>, Type: Train                 
      midlandboxcar05x, Type: Train          
      CABOOSE01X, Type: Train                
      CANOE, Type: Boat                      
      CANOETREETRUNK, Type: Boat             
      CART01, Type: Coach                    
      CART02, Type: Coach                    
      CART03, Type: Coach                    
      CART04, Type: Coach                    
      CART05, Type: Coach                    
      CART06, Type: Coach                    
      CART07, Type: Coach                    
      CART08, Type: Coach                    
      COACH2, Type: Coach                    
      COACH3, Type: Coach                    
      coach3_cutscene, Type: Coach             Same as COACH3 but less worn
      COACH4, Type: Coach                    
      COACH5, Type: Coach                    
      COACH6, Type: Coach                    
      BUGGY01, Type: Coach                   
      BUGGY02, Type: Coach                   
      BUGGY03, Type: Coach                   
      ARMYSUPPLYWAGON, Type: Coach           
      CHUCKWAGON000X, Type: Coach            
      supplywagon, Type: Coach               
      supplywagon2, Type: Coach                Supply trailer?
      LOGWAGON, Type: Coach                  
      <unknown>, Type: Coach                   Also a log wagon
      coal_wagon, Type: Coach                
      CHUCKWAGON002X, Type: Coach            
      GATLING_GUN, Type: Other               
      GATLINGMAXIM02, Type: Other            
      HANDCART, Type: Other                  
      horseBoat, Type: Boat                  
      HOTAIRBALLOON01, Type: Other           
      hotchkiss_cannon, Type: Other          
      <unknown>, Type: Other                   Mining cart
      northflatcar01x, Type: Train           
      privateflatcar01x, Type: Train         
      northpassenger01x, Type: Train         
      northpassenger03x, Type: Train         
      privatepassenger01x, Type: Train       
      OILWAGON01X, Type: Coach               
      OILWAGON02X, Type: Coach               
      PIROGUE, Type: Boat                    
      PIROGUE2, Type: Boat                   
      POLICEWAGON01X, Type: Coach            
      <unknown>, Type: Coach                   Same as POLICEWAGON01X but with a cannon
      PRIVATECOALCAR01X, Type: Train         
      northcoalcar01x, Type: Train           
      WINTERSTEAMER, Type: Train             
      wintercoalcar, Type: Train             
      privateboxcar04x, Type: Train          
      privateboxcar02x, Type: Train          
      privateboxcar01x, Type: Train          
      <unknown>, Type: Train                 
      privateobservationcar, Type: Train     
      privatearmoured, Type: Train           
      PRIVATEDINING01X, Type: Train          
      <unknown>, Type: Train                 
      PRIVATESTEAMER01X, Type: Train         
      NORTHSTEAMER01X, Type: Train           
      <unknown>, Type: Train                 
      <unknown>, Type: Train                 
      <unknown>, Type: Train                 
      <unknown>, Type: Train                 
      RCBOAT, Type: Boat                     
      rowboat, Type: Boat                    
      ROWBOATSWAMP, Type: Boat               
      ROWBOATSWAMP02, Type: Boat              A worn row boat
      SHIP_GUAMA02, Type: Boat               
      turbineboat, Type: Boat                
      SHIP_NBDGUAMA, Type: Boat              
      <unknown>, Type: Boat                   Big Ship
      SKIFF, Type: Boat                      
      STAGECOACH001X, Type: Coach            
      STAGECOACH002X, Type: Coach            
      STAGECOACH003X, Type: Coach            
      STAGECOACH004X, Type: Coach            
      STAGECOACH005X, Type: Coach            
      STAGECOACH006X, Type: Coach            
      trolley01x, Type: Train                  Saint Denise Tram
      <unknown>, Type: Boat                   Big Ship
      WAGON02X, Type: Coach
      WAGON03X, Type: Coach
      WAGON04X, Type: Coach
      WAGON05X, Type: Coach
      WAGON06X, Type: Coach
      wagonCircus01x, Type: Coach
      wagonCircus02x, Type: Coach
      wagonDoc01x, Type: Coach
      WAGONPRISON01X, Type: Coach
      wagonWork01x, Type: Coach
      wagonDairy01x, Type: Coach
      wagonTraveller01x, Type: Coach
      BREACH_CANNON, Type: Other
      UTILLIWAG, Type: Coach
      GATCHUCK, Type: Coach
      GATCHUCK_2, Type: Coach*/
}
