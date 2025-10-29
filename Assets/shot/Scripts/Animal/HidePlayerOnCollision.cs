using UnityEngine;
using System.Collections; // �ʿ� ���ӽ����̽� �߰�

public class HidePlayerOnCollision : MonoBehaviour
{
    public bool bActive = true; // Ȱ�� ���� ����
    public Material newMaterial; // �ٸ� ���͸����� ����
    // public Sig sigInstance; // Sig Ŭ���� �ν��Ͻ� ����
    public GameObject gunObject;
    public bool isfly;
    private Renderer objectRenderer;

    // Ʈ���� �ݶ��̴��� �ٸ� �ݶ��̴��� ���� �� ȣ��Ǵ� �޼ҵ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered"); // ������ �α�

        // �浹�� ��ü�� "Ball" �±׸� ������ �ִ��� Ȯ��
        if (other.CompareTag("Ball"))
        {
            // ������Ʈ�� Renderer ������Ʈ�� ������
            objectRenderer = GetComponent<Renderer>();
            // �ڷ�ƾ ����
            StartCoroutine(HideAndMoveBack(other.gameObject));
        }
    }

    private IEnumerator HideAndMoveBack(GameObject obj)
    {
        //user input 0

        if (this.tag != "Ground")
        {
            // ������Ʈ�� ����� ȭ�鿡�� �Ⱥ��̰� �ϱ� ���� Renderer�� ��Ȱ��ȭ
            objectRenderer.enabled = false;
        }

        //user input 0 end

        // ��ü�� ������ ������Ʈ�� ������
        Renderer objRenderer = obj.GetComponent<Renderer>();

        // ���� ���͸��� ����
        Material originalMaterial = objRenderer.material;

        // �ٸ� ���͸���� ����
        objRenderer.material = newMaterial;
        Debug.Log("Material changed");

        // ��ü�� Rigidbody2D ������Ʈ�� ������
        Rigidbody2D objRigidbody = obj.GetComponent<Rigidbody2D>();

        // ��ü�� ��Ȱ��ȭ
        obj.SetActive(false);
        Debug.Log("Ball deactivated");

        // ��ü�� (0,0) ��ġ�� �̵�
        objRigidbody.gravityScale = 0f;
        if (gunObject != null)
        {
            // Get the position of the target object
            obj.transform.position = gunObject.transform.position;

        }

        Animal animal = GetComponent<Animal>();
        switch (this.tag)
        {
            case "Sarg":
                animal.Ani(0);
                break;
            case "Bebsae":
                animal.Ani(1);
                break;
            case "Fox":
                animal.Ani(2);
                break;
            case "Dambe":
                animal.Ani(3);
                break;
            case "Quokka":
                animal.Ani(4);
                break;
            case "MangCong":
                animal.Ani(5);
                break;
            case "Namsang":
                animal.Ani(6);
                break;
            case "Bat":
                animal.Ani(7);
                break;
            case "Osori":
                animal.Ani(8);
                break;
            case "Goat":
                animal.Ani(9);
                break;
            case "Bigbard":
                animal.Ani(10);
                break;
            case "Therebird":
                animal.Ani(11);
                break;
            case "Pigeon":
                animal.Ani(12);
                break;
            case "Glassmonkey":
                animal.Ani(13);
                break;
            case "Coyote":
                animal.Ani(14);
                break;
            case "Negul":
                animal.Ani(15);
                break;
            case "Gorani":
                animal.Ani(16);
                break;
            case "Frogbird":
                animal.Ani(17);
                break;
            case "Bear":
                animal.Ani(18);
                break;
            case "Bluebird":
                animal.Ani(19);
                break;
            case "Thembugi":
                animal.Ani(20);
                break;
            case "Mudangbird":
                animal.Ani(21);
                break;
            case "Redheart":
                animal.Ani(22);
                break;
            case "Bluetigerbird":
                animal.Ani(23);
                break;
            case "Hosabiori":
                animal.Ani(24);
                break;
            default:
                break;
        }

        // 3�� ���
        yield return new WaitForSeconds(3);

        // ���� ���͸���� �ǵ���
        objRenderer.material = originalMaterial;
        Debug.Log("Material reverted");

        // ��ü�� �ٽ� Ȱ��ȭ
        obj.SetActive(true);
        Debug.Log("Ball reactivated at (-2.5,)");

        // Rigidbody2D�� �ٽ� Ȱ��ȭ
        if (objRigidbody != null)
        {
            objRigidbody.simulated = true;
        }

        bActive = true; // Ȱ�� ���� ������Ʈ

        if (this.tag != "Ground")
        {
            // �� ������Ʈ ��ȯ
            if (isfly == true)
            {
                Spawner.instance.Fly_Spawn();
            }
            else
            {
                Spawner.instance.Ground_Spawn();
            }
            isfly = false;

            // �� ���� ������Ʈ ����
            Destroy(gameObject);
        }
    }
}
