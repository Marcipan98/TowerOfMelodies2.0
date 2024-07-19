using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 public static class GameData
{

    private static bool ownsItem = false;

    public static void playerGotTheKey() {
        ownsItem = true;
    }

    public static void playerLostTheKey() {
        ownsItem = false;
    }

    public static bool doesPlayerOwnKey() {
        return ownsItem;
    }
}
