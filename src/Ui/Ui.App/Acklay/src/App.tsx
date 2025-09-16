import { useEffect, useState } from "react";
import { Canvas } from "@react-three/fiber";
import { OrbitControls } from "@react-three/drei";
import { AxesHelper, GridHelper } from "three";
import type { PositionsResponse } from "./types";

type Vector3Tuple = [number, number, number];

function Body({ position, color }: { position: Vector3Tuple; color: string }) {
  return (
    <mesh position={position}>
      <sphereGeometry args={[0.8, 32, 32]} />
      <meshStandardMaterial color={color} metalness={0.3} roughness={0.5} />
      <mesh>
        <sphereGeometry args={[0.82, 16, 16]} />
        <meshBasicMaterial color="white" wireframe />
      </mesh>
    </mesh>
  );
}

export default function App() {
  const [positions, setPositions] = useState<Vector3Tuple[]>([]);
  const colors = ["red", "green", "blue"];

  useEffect(() => {
    const fetchPositions = async () => {
      try {
        const res = await fetch(
          "https://localhost:7222/simulation/GetNextPositions"
        );
        const data: PositionsResponse = await res.json();

        const newPositions: Vector3Tuple[] = Object.values(data.positions).map(
          (p) => [p.x, p.y, p.z]
        );

        setPositions(newPositions);
      } catch (err) {
        console.error("Error fetching positions:", err);
      }
    };

    fetchPositions();
    const interval = setInterval(fetchPositions, 100);
    return () => clearInterval(interval);
  }, []);

  return (
    <div style={{ width: "100vw", height: "100vh" }}>
      <Canvas camera={{ position: [0, 0, 25], fov: 60 }}>
        <ambientLight intensity={0.7} />
        <pointLight position={[10, 10, 10]} intensity={1} />
        <OrbitControls />
        <primitive object={new AxesHelper(12)} />
        <primitive object={new GridHelper(20, 20)} />
        {positions.map((pos, i) => (
          <Body key={i} position={pos} color={colors[i % colors.length]} />
        ))}
      </Canvas>
    </div>
  );
}
