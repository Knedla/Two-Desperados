using UnityEngine;

public class FirewallNodeController : NodeController
{
    [SerializeField] private TracerController TracerController;
    // jel uopste igrac moze da hakuje firewall
    // sta se desi kad je firewall hakovan
    // jel znaci da je i tracer "ubijen"
    // sta ako je tracer vec krenuo a ti posle toga zauzmes firewall
    // pravicu se blesav i ostaviti sve kako jeste, al eto, mogu i te stvari da se menjaju
}
