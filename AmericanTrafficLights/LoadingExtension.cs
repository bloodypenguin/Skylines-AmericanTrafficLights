using ICities;
using UnityEngine;

namespace AmericanTrafficLights
{
    public class LoadingExtension : LoadingExtensionBase
    {

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
            {
                return;
            }
            var mainLight = PrefabCollection<PropInfo>.FindLoaded("694123443.AmericanTrafficLightMain_Data");
            var walkLight = PrefabCollection<PropInfo>.FindLoaded("694123443.AmericanTrafficLightWalk_Data");
            if (mainLight == null || walkLight == null)
            {
                return;
            }
            var roads = Resources.FindObjectsOfTypeAll<NetInfo>();
            foreach (var road in roads)
            {
                if (road.m_lanes == null)
                {
                    return;
                }
                foreach (var lane in road.m_lanes)
                {
                    if (lane?.m_laneProps?.m_props == null)
                    {
                        continue;
                    }
                    foreach (var laneProp in lane.m_laneProps.m_props)
                    {
                        var prop = laneProp.m_finalProp;
                        if (prop == null)
                        {
                            continue;
                        }
                        var name = prop.name;
                        if (name == "Traffic Light European 02" || name == "Traffic Light 02")
                        {
                            laneProp.m_finalProp = mainLight;
                            laneProp.m_prop = mainLight;
                        }
                        if (name == "Traffic Light European 01" ||
                            name == "Traffic Light Pedestrian European" ||
                            name == "Traffic Light European 01 Mirror" ||
                            name == "Traffic Light European 02 Mirror" ||
                            name == "Traffic Light 01" || name == "Traffic Light Pedestrian" ||
                            name == "Traffic Light 01 Mirror" || name == "Traffic Light 02 Mirror")
                        {
                            laneProp.m_finalProp = walkLight;
                            laneProp.m_prop = walkLight;
                        }
                    }
                }
            }
        }
    }
}