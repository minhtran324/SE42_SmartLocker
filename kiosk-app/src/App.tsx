import { NavLink, Route, Routes } from "react-router-dom";
import VerifyIdentityPage from "./features/verify-identity";
import DepositPage from "./features/deposit";
import RetrievePage from "./features/retrieve";

const NAV_ITEMS = [
  { to: "/", label: "Verify Identity", element: <VerifyIdentityPage /> },
  { to: "/deposit", label: "Deposit", element: <DepositPage /> },
  { to: "/retrieve", label: "Retrieve", element: <RetrievePage /> },
];

// UC-K01: Start Kiosk Session — TODO: on app boot, ping the backend to decide
// Online vs Offline mode before rendering these routes.
export default function App() {
  return (
    <div style={{ fontFamily: "sans-serif", padding: 24 }}>
      <header style={{ display: "flex", gap: 16, marginBottom: 24 }}>
        <h1 style={{ fontSize: 18 }}>SLMS Kiosk</h1>
        <nav style={{ display: "flex", gap: 12 }}>
          {NAV_ITEMS.map((item) => (
            <NavLink key={item.to} to={item.to} end={item.to === "/"}>
              {item.label}
            </NavLink>
          ))}
        </nav>
      </header>
      <main>
        <Routes>
          {NAV_ITEMS.map((item) => (
            <Route key={item.to} path={item.to} element={item.element} />
          ))}
        </Routes>
      </main>
    </div>
  );
}
