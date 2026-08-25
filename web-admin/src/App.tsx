import { NavLink, Route, Routes } from "react-router-dom";
import DashboardPage from "./features/dashboard";
import StationsPage from "./features/stations";
import LockersPage from "./features/lockers";
import BookingsPage from "./features/bookings";
import IncidentsPage from "./features/incidents";
import PricingPage from "./features/pricing";
import UsersPage from "./features/users";
import AuditLogPage from "./features/audit-log";

const NAV_ITEMS = [
  { to: "/", label: "Dashboard", element: <DashboardPage /> },
  { to: "/stations", label: "Stations", element: <StationsPage /> },
  { to: "/lockers", label: "Lockers & Devices", element: <LockersPage /> },
  { to: "/bookings", label: "Bookings & Payments", element: <BookingsPage /> },
  { to: "/incidents", label: "Incidents", element: <IncidentsPage /> },
  { to: "/pricing", label: "Pricing", element: <PricingPage /> },
  { to: "/users", label: "Users & Roles", element: <UsersPage /> },
  { to: "/audit-log", label: "Audit Log", element: <AuditLogPage /> },
];

export default function App() {
  return (
    <div style={{ display: "flex", minHeight: "100vh", fontFamily: "sans-serif" }}>
      <nav style={{ width: 220, borderRight: "1px solid #ddd", padding: 16 }}>
        <h2 style={{ fontSize: 16 }}>SLMS Admin</h2>
        <ul style={{ listStyle: "none", padding: 0 }}>
          {NAV_ITEMS.map((item) => (
            <li key={item.to} style={{ margin: "8px 0" }}>
              <NavLink to={item.to} end={item.to === "/"}>
                {item.label}
              </NavLink>
            </li>
          ))}
        </ul>
      </nav>
      <main style={{ flex: 1, padding: 24 }}>
        <Routes>
          {NAV_ITEMS.map((item) => (
            <Route key={item.to} path={item.to} element={item.element} />
          ))}
        </Routes>
      </main>
    </div>
  );
}
