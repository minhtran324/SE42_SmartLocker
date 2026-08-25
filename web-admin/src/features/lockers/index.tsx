// UC-A04: 4-state locker view (Available/Occupied/Reserved/Maintenance).
// UC-A07: device health — heartbeat freshness, MQTT connectivity, firmware, RTC drift.
// UC-A11: set/clear maintenance, trigger hardware self-test.
export default function LockersPage() {
  return (
    <section>
      <h1>Lockers &amp; Devices</h1>
      <p>TODO (UC-A04): locker grid per station with status filter.</p>
      <p>TODO (UC-A07): device health table from GET /api/lockers/device-health.</p>
      <p>TODO (UC-A11): maintenance toggle calling POST /api/lockers/:id/maintenance.</p>
    </section>
  );
}
